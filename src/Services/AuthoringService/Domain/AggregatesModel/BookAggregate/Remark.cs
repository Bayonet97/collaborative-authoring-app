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
        public Guid Id { get; }

        private string remarkedText;

        private int startPosition;
        private int endPosition;

        private Guid bookId;
        private Guid pageId;

        public Guid BookId { get => bookId; set { bookId = value; } }
        public Guid PageId { get => pageId; set{ pageId = value; } }

        public string RemarkedText
        {
            get => remarkedText;
        }

        public int StartPosition { get => startPosition; }
        public int EndPosition { get => endPosition; }

        public Remark()
        {

        }

        public Remark(Guid guid, string _text, (int,int) _position, Page _page)
        {
            Id = guid;
            remarkedText = _text;
            startPosition = _position.Item1;
            endPosition = _position.Item2;
            PageId = _page.Id;
            BookId = _page.BookId;
            _page.TextChangedEvent += ValidatePosition;
        }

        private void ValidatePosition(object sender, TextChangedDomainEvent textChangeEvent)
        {
            if(startPosition > textChangeEvent.Position)
            {
                int addedCount = textChangeEvent.NewText.Length;
                UpdatePosition((startPosition + addedCount, endPosition + addedCount));
            }
            if(startPosition <= textChangeEvent.Position && endPosition >= textChangeEvent.Position)
            {
                char[] chars = remarkedText.ToCharArray();
                int shortest = Math.Min(RemarkedText.Length, textChangeEvent.NewText.Length);
                for (int i = 0; i < shortest; i++)
                {
                    if(remarkedText[i] != textChangeEvent.NewText[i])
                    {
                        chars[i] = textChangeEvent.NewText[i];
                    }
                }

                remarkedText = chars.ToString();
                RemarkChangedDomainEvent.RemarkChanged(this);
            }
        }

        public void UpdatePosition((int,int) newPosition)
        {
            startPosition = newPosition.Item1;
            endPosition = newPosition.Item2;
            RemarkChangedDomainEvent.RemarkChanged(this);
        }

        public void UpdateText(string newText, (int,int) newPosition)
        {
            remarkedText = newText;
            startPosition = newPosition.Item1;
            endPosition = newPosition.Item2;
            RemarkChangedDomainEvent.RemarkChanged(this);
        }

    }
}
