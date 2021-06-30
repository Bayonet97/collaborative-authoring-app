using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.RemarkService.Domain.AggregatesModel.BookAggregate
{
    public class Remark
    {
        private Guid id;
        public Guid Id { get => id; set => id = value; }

        private string remarkedText;
        private string remark;

        private int startPosition;
        private int endPosition;

        private Guid bookId;
        private Guid pageId;

        public Guid BookId { get => bookId; set => bookId = value; }
        public Guid PageId { get => pageId; set { pageId = value; } }


        public string RemarkedText
        {
            get => remarkedText;
            set => remarkedText = value;
        }

        public string RemarkString { get => remark; set => remark = value; }

        public int StartPosition { get => startPosition; set => startPosition = value; }
        public int EndPosition { get => endPosition; set => endPosition = value; }

        public Remark()
        {

        }

        public Remark(Guid guid, string remarkedText, (int, int) position, Guid pageId, Guid bookId)
        {
            Id = guid;
            this.remarkedText = remarkedText;
            startPosition = position.Item1;
            endPosition = position.Item2;
            PageId = pageId;
            BookId = bookId;
        }

        public void UpdateRemark(int newStartPosition, int newEndPosition, string newText, Guid? newPageId)
        {
            startPosition = newStartPosition;
            endPosition = newEndPosition;
            remarkedText = newText;

            if (newPageId != null)
                pageId = (Guid)newPageId;
        }
    }
}
