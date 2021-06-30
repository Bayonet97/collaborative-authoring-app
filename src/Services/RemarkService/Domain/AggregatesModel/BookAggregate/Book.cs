using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.RemarkService.Domain.AggregatesModel.BookAggregate
{
    public class Book
    {
        private Guid id;
        public Guid Id { get => id; set => id = value; }

        private List<Remark> remarks = new();

        public List<Remark> Remarks { get => remarks; set => remarks = value; }

        public Book()
        {
        }

        public Book(Guid id)
        {
            Id = id;
        }

        public void AddRemark(Remark remark)
        {
            Remarks.Add(remark);
        }

        public void RemoveRemark(Remark remark)
        {
            Remarks.Remove(remark);
        }

        public void UpdateRemarkPosition(Remark remark)
        {
            Remark r = Remarks[Remarks.FindIndex(r => r.Id == remark.Id)];
            r.PageId = remark.PageId;
            r.RemarkedText = remark.RemarkedText;
            r.StartPosition = remark.StartPosition;
            r.EndPosition = remark.EndPosition;

            Remarks[Remarks.FindIndex(r => r.Id == remark.Id)] = r;
        }
    }
}
