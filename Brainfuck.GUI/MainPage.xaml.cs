using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Brainfuck.Library;
using Brainfuck.Shared;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Brainfuck.GUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, IInputOutput
    {
        private readonly IBrainfuck _brainfuck;

        public MainPage()
        {
            _brainfuck = BrainfuckInterpreter.Create(this);
            this.InitializeComponent();
            RichEditBox richEditBox = BrainfuckInputRichTextBox;
            richEditBox.CharacterReceived += RichEditBox_CharacterReceived;
        }
        
        public async Task Output(string data)
        {
            OutputRichTextBox.Document.GetText(TextGetOptions.NoHidden, out string input);
            OutputRichTextBox.Document.SetText(TextSetOptions.Unhide, input + data);
        }

        private int _position;

        public async Task<char> Input()
        {
            char[] c = InputTextBox.Text.ToCharArray();
            try
            {
                if (_position > c.Length - 1)
                    return (char) 0;

                return c[_position];
            }
            finally
            {
                _position++;
            }
        }

        private async void UIElement_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.F5)
            {
                OutputRichTextBox.Document.SetText(TextSetOptions.Unhide, "");
                _position = 0;
                BrainfuckInputRichTextBox.Document.GetText(TextGetOptions.NoHidden, out string input);
                using (ICpu cpu = _brainfuck.CreateCpu(input))
                {
                    bool failed = false;
                    try
                    {
                        await cpu.Process();
                    }
                    catch (Exception ex)
                    {
                        var dialog = new MessageDialog("Error: " + ex.Message);
                        await dialog.ShowAsync();
                        failed = true;
                    }

                    MemoryDumpListBox.Items.Clear();
                    foreach (var v in cpu.Memory.Dump())
                    {
                        MemoryDumpListBox.Items.Add($"{v.ToString()} ({(char)v})");
                    }

                    if(!failed)
                    MemoryDumpListBox.SelectedIndex = (int)cpu.Memory.Address;
                }
            }
        }

        private void ChangeTextColor(string text, Color color)
        {
            BrainfuckInputRichTextBox.Document.GetText(TextGetOptions.None, out string input);

            var myRichEditLength = input.Length;

            BrainfuckInputRichTextBox.Document.Selection.SetRange(0, myRichEditLength);
            int i = 1;
            while (i > 0)
            {
                i = BrainfuckInputRichTextBox.Document.Selection.FindText(text, myRichEditLength, FindOptions.Case);

                ITextSelection selectedText = BrainfuckInputRichTextBox.Document.Selection;
                if (selectedText != null)
                {
                    selectedText.CharacterFormat.BackgroundColor = color;
                }
            }
        }
        
        private void BrainfuckInputRichTextBox_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private IDictionary<char, Color> _syntaxHighlight = new Dictionary<char, Color>()
        {
            // Operations
            ['+'] = Colors.Red,
            ['-'] = Colors.DarkRed,
            // Loop
            ['['] = Colors.Blue,
            [']'] = Colors.Blue,
            // Pointer operations
            ['>'] = Colors.Green,
            ['<'] = Colors.DarkGreen,
        };

        private void RichEditBox_CharacterReceived(UIElement sender, CharacterReceivedRoutedEventArgs args)
        {
            Color defaultColor = Colors.Black;

            if (_syntaxHighlight.TryGetValue(args.Character, out Color color))
                DefineColor(BrainfuckInputRichTextBox, color);
            else if (args.Character != '\b')
                DefineColor(BrainfuckInputRichTextBox, defaultColor);

            args.Handled = false;
        }

        private void DefineColor(RichEditBox box, Color color)
        {
            var startPos = box.Document.Selection.StartPosition -= 1;
            box.Document.Selection.EndPosition = startPos + 1;
            box.Document.Selection.CharacterFormat.ForegroundColor = color;
            box.Document.Selection.StartPosition += 1;
            //box.Document.Selection.CharacterFormat.ForegroundColor = color;
        }
    }
}
