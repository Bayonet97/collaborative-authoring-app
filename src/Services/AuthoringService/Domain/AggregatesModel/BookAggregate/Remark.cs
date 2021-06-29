using CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate.Events;
using CA.Services.AuthoringService.Domain.Events;
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

        private (int start, int end) position;

        private Guid pageId;

        public Guid PageId { get => pageId; set{ pageId = value; } }

        public string Text
        {
            get => text;
        }

        public (int,int) Position { get => position; }

        public Remark()
        {

        }

        public Remark(Guid guid, string _text, (int,int) _position, Page _page)
        {
            Id = guid;
            text = _text;
            position = _position;
            PageId = _page.Id;
            _page.TextChangedEvent += ValidatePosition;
        }

        private void ValidatePosition(object sender, TextChangedDomainEvent textChangeEvent)
        {
            if(position.start > textChangeEvent.Position)
            {
                int addedCount = textChangeEvent.NewText.Length;
                UpdatePosition((position.start + addedCount, position.end + addedCount));
            }
            if(position.start <= textChangeEvent.Position && position.end >= textChangeEvent.Position)
            {
                char[] chars = text.ToCharArray();
                int shortest = Math.Min(Text.Length, textChangeEvent.NewText.Length);
                for (int i = 0; i < shortest; i++)
                {
                    if(text[i] != textChangeEvent.NewText[i])
                    {
                        chars[i] = textChangeEvent.NewText[i];
                    }
                }

                text = chars.ToString();
                RemarkChangedDomainEvent.RemarkChanged(this);
            }
        }

        public void UpdatePosition((int,int) newPosition)
        {
            position = newPosition;
            RemarkChangedDomainEvent.RemarkChanged(this);
        }

        public void UpdateText(string newText, (int,int) newPosition)
        {
            text = newText;
            position = newPosition;
            RemarkChangedDomainEvent.RemarkChanged(this);
        }

    }
}
