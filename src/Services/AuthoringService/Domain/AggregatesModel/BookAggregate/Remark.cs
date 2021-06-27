using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate
{
    public class Remark
    {
        public Guid Id;

        private string text;

        private int[,] position;

        private Page page;

        public Page Page { get => page; set{ page = value; } }

        public string Text
        {
            get => text;
        }

        public int[,] Position { get => position; }

        public Remark()
        {

        }

        public Remark(Guid guid, string _text, int[,] _position, Page _page)
        {
            Id = guid;
            text = _text;
            position = _position;
            Page = _page;
        }

        public void UpdatePosition(int[,] newPosition)
        {
            position = newPosition;
        }

        public void UpdateText(string newText, int[,] newPosition)
        {
            text = newText;
            position = newPosition;
        }
    }
}
