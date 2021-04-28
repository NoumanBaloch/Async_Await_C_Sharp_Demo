using AsyncAwaitBestPractices;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwaitDemo
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class AsynAwaitBestRefactoring
    {
        //Refactoring Constructor
        public AsynAwaitBestRefactoring()
        {
            ExecuteInitialization().SafeFireAndForget();
        }

        async Task ExecuteInitialization()
        {
            await Task.Delay(3000);
        }
        async Task FirstRefactoring()
        {
            //First Refactoring
            //IsValid(true).Wait();

            #region After Refactoring
            //just user await keyword
            await IsValid(true);
            //If you really want to wait then use following
            IsValid(true).GetAwaiter().GetResult();
            #endregion

        }

        async Task SecondRefactoring()
        {
            //Second Refactoring
            //List<int> Identifiers = await GetAllIds(); //thread 10

            //foreach(var id in Identifiers)
            //{
            //    await GetDetailsById(id); // thread 20
            //}

            #region After Refactoring

            List<int> Identifiers = await GetAllIds().ConfigureAwait(false); //thread 20

            foreach (var id in Identifiers)
            {
                await GetDetailsById(id).ConfigureAwait(false);
            }
            #endregion
        }

        Task<byte[]> ThirdRefactoring()
        {
            //Third Refactoring
            //WebClient wc = new WebClient();
            //return wc.DownloadDataTaskAsync("https://www.slideshare.net/NoumanBaloch5/dining-philosopher-problem-124674804");

            #region After Refactoring
            WebClient wc = new WebClient();
            return wc.DownloadDataTaskAsync("https://www.slideshare.net/noumanbaloch5/dining-philosopher-problem-124674804");

            #endregion
        }

        async Task<byte[]> FourthRefactoring()
        {
            try
            {
                WebClient wc = new WebClient();
                return await wc.DownloadDataTaskAsync("https://www.slideshare.net/NoumanBaloch5/dining-philosopher-problem-124674804");

            }
            catch (Exception)
            {

                throw new Exception();
            }
            
        }

        async Task<Person> GetDetailsById(int Id)
        {
            List<Person> person = new List<Person>
            {
                new Person { ID = 1, Name = "Steve", Age = 24},
                new Person { ID = 3, Name = "John", Age = 20},
                new Person { ID = 3, Name = "David", Age = 30}

            };
            await Task.Delay(3000);
            return person.Find(x => x.ID == Id);
        }
        private async Task<List<int>> GetAllIds()
        {
            List<int> Ids = new List<int> { 1, 2, 3 };
            await Task.Delay(3000);
            return Ids;
        }

        private async Task<bool> IsValid(bool input)
        {
            await Task.Delay(2000);
            return true;
        }
    }

   
}
