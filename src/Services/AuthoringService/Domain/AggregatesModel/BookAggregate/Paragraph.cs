using CA.Services.AuthoringService.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.Domain.AggregatesModel.BookAggregate
{
    public class Paragraph
    {
        public Guid Id;

        private string text;

        private Guid remarkId;

        public Guid RemarkId
        {
            get => remarkId;
            set
            {
                if (value == Guid.Empty)
                    throw new BookDomainException("Remark Id is empty");
                remarkId = value;
            }
        }

        public string Text
        {
            get => text;
        }
        
        public Paragraph()
        {

        }

        public Paragraph(Guid paragraphId)
        {
            Id = paragraphId;
        }

        public string AddText(int position, string text)
        {
            if(text.Length != 0 && text.Length <= position)
            {
                position = text.Length - 1;
            }
            else if(text.Length == 0)
            {
                position = 0;
            }

            text.Insert(position, text);

            return Text;
        }

        public string RemoveText(int position, int count)
        {
            if(text.Length == 0)
            {
                return "";
            }
            else if(text.Length <= position)
            {
                position = text.Length - 1;
            }

            text.Remove(position, count);
            return Text;
        }
    }
}
