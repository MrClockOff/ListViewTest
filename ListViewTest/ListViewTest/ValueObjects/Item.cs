using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ListViewTest.Annotations;

namespace ListViewTest.ValueObjects
{
    public class Item : INotifyPropertyChanged
    {
        public string Title { get; }
        public string ImageUrl { get; }

        public Item(string namePrefix = null)
        {
            Title = $"{DateTime.Now}--{namePrefix}";
            ImageUrl = GetImageUrl();
        }

        private static string GetImageUrl()
        {
            var urls = new []
            {
                "https://cdn1.iconfinder.com/data/icons/ninja-things-1/1772/ninja-simple-512.png",
                "https://s-media-cache-ak0.pinimg.com/236x/9e/1b/a8/9e1ba8be0d819126f5387a510e0f96eb.jpg",
                "https://cdn1.iconfinder.com/data/icons/user-pictures/100/male3-512.png",
                "http://image.flaticon.com/icons/png/128/145/145867.png",
                "http://findicons.com/files/icons/1072/face_avatars/300/i03.png"
            };
            var random = new Random();
            var index = random.Next(4);
            return urls[index];
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}