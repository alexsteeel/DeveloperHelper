using System;
using System.Drawing;

namespace ProjectManagementModule
{
    public class TextLogEventArgs : EventArgs
    {
        public TextLogEventArgs(string message, Color color)
        {
            Message = message;
            Color = color;
        }

        public string Message { get; }
        public Color Color { get; }
    }
}
