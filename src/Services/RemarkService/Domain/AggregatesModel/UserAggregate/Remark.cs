using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.RemarkService.Domain.AggregatesModel.UserAggregate
{
    public class Remark
    {
        private Guid id;
        public Guid Id { get => id; set => id = value; }

        private string text;

        private int startPosition;
        private int endPosition;

        private Guid pageId;

        public Guid PageId { get => pageId; set { pageId = value; } }

        public string Text
        {
            get => text;
            set => text = value;
        }
        public int StartPosition { get => startPosition; set => startPosition = value; }
        public int EndPosition { get => endPosition; set => endPosition = value; }

        public Remark()
        {

        }

        public Remark(Guid guid, string _text, (int, int) _position, Guid pageId)
        {
            Id = guid;
            text = _text;
            startPosition = _position.Item1;
            endPosition = _position.Item2;
            PageId = pageId;
        }

    }
}
