using Microsoft.AspNetCore.Components;
using PhoneBook.Models.Dtos;

namespace PhoneBook.Web.Pages
{
    public class DisplayContactsBase:ComponentBase
    {
        [Parameter]
        public IEnumerable<ContactDto> Contacts { get; set;}
    }
}
