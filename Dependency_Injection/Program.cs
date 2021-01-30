using System;

namespace Dependency_Injection
{
    class Program
    {
        static void Main(string[] args)
        {
            // (*)
            Vasita vasitaAraba = new Vasita(new Araba());
            vasitaAraba.Kullan();

            Vasita vasitaOtobus = new Vasita(new Otobus());
            vasitaOtobus.Kullan();

            Vasita vasitaMotor = new Vasita(new Motor());
            vasitaMotor.Kullan();


            // (**)


        }

        /*
        class Araba
        {
            public void GazVer()
            {
                //...
            }
            public void FrenYap()
            {
                //...
            }
            public void SagaSinyal()
            {
                //...
            }
            public void SolaSinyal()
            {
                //..
            }
        }

        class Vasita
        {
            Araba araba;
            public Vasita()
            {
                araba = new Araba();
            }

            public void Kullan()
            {
                araba.GazVer();
                araba.SagaSinyal();
                araba.FrenYap();
                araba.SolaSinyal();
            }
        }
        */

        /*
         * Yukarıdaki kod bloğunu incelerseniz “Vasita” sınıfı “Araba” sınıfına bağlı bir vaziyet arz etmektedir. Yani ben ne zaman “Araba” sınıfında bir değişiklik yapsam gelip “Vasita” sınıfında da o değişikliğe göre çalışma gerçekleştirmem gerekebilir. 
           Haliyle onlarca sınıf söz konusu olduğu durumlarda bu pek mümkün olmayacaktır.

Şimdi düşünün ki, “Vasita” sınıfına “Araba” yerine “Otobus” classı vermek durumunda kalırsam eğer gelip burada ki komutları güncellemek zorunda kalacağım. Onlarca sınıf durumunda bağımlılık arz eden sınıflarda güncelleme yapmak hiç yazılımcı işi değildir.

İşte… “Araba sınıfı istediği kadar değişsin ama Vasita sınıfının bundan haberi olmasın. Haberi olmasın ki Vasita sınıfıyla uğraşmak zorunda kalmayayım” diyorsak eğer Dependency Injection(DI) tasarımını uygulayacağız.
         */

        /*
         * Şimdi DI desenini uygulamak için bir interface tanımlayacağız. Tanımladığımız bu Interface’den kalıtım alan tüm sınıflar doğal olarak Interface’in kalıbını, şartını sağlayacaktır.
         */

        interface ITasit
        {
            void GazVer();
            void FrenYap();
            void SagaSinyal();
            void SolaSinyal();
        }

        //Evet, gördüğünüz gibi Interface’imizi oluşturduk. Şimdi “Araba”, “Otobus”, “Motor” vs. gibi taşıtlarımızı bu Interface’den türeteceğiz.

        class Araba : ITasit
        {
            public void GazVer()
            {
                //...
            }
            public void FrenYap()
            {
                //...
            }
            public void SagaSinyal()
            {
                //...
            }
            public void SolaSinyal()
            {
                //..
            }
        }

        class Otobus : ITasit
        {
            public void GazVer()
            {
                //...
            }
            public void FrenYap()
            {
                //...
            }
            public void SagaSinyal()
            {
                //...
            }
            public void SolaSinyal()
            {
                //..
            }
        }

        class Motor : ITasit
        {
            public void GazVer()
            {
                //...
            }
            public void FrenYap()
            {
                //...
            }
            public void SagaSinyal()
            {
                //...
            }
            public void SolaSinyal()
            {
                //..
            }
        }

        //Şimdi vakit “Vasita” sınıfımıza DI desenini uygulamaya geldi.

        class Vasita
        {
            ITasit _tasit;
            public Vasita(ITasit tasit)
            {
                _tasit = tasit;
            }

            public void Kullan()
            {
                _tasit.GazVer();
                _tasit.SagaSinyal();
                _tasit.FrenYap();
                _tasit.SolaSinyal();
            }
        }

        /*
         * Yukarıdaki kod bloğunu incelerseniz eğer, “ITasit” Interface’inden kalıtım alan herhangi bir sınıf “Vasita” sınıfına bir taşıt olabilir. Haliyle siz proje sürecinde “Vasita” sınıfına ister “Araba”, isterseniz “Otobus” yahut “Motor” verebilirsiniz. Nede olsa hepsi aynı metod ve işlevleri barındırdığı için hangi nesneyi verirseniz verin Interface referansı üzerinden ilgili nesne çalıştırılacaktır.

Olaya “Vasita” sınıfı açısından bakarsak eğer, kendisine verilen nesnenin ne olduğuyla ilgilenmemekte ve bağlı olduğu bir sınıf bulunmamaktadır. Yani olay araba, otobüs veyahut motor da olsa bu elemanların çalışma ve işleyişiyle ilgilenmemektedir. Nihayetinde araba ve otobüste gaz vermek için pedala basılırken, motorda sağ kolu çevirmek lazımdır. İşte Dependency Injection sayesinde “Vasita” sınıfı bu işlevlerin nasıl yapıldığıyla ilgilenmemekte, sadece ve sadece gelen araç hangisi olursa olsun gaz vermekte, frene basmakta ve sağa sola sinyal çakmaktadır. Eğer DI uygulanmazsa, “Vasita” gaz verme işleminde araba ve otobüs için pedalla, motor içinse sağ kolla ilgilenecektir.

Anlayacağınız “Vasita” sınıfı Dependency Injection sayesinde üzümü yiyor bağını sormuyor, farklı bir sınıfa olan bağını minimize etmiş oluyor.
         */

        //Bundan sonra yukarıda (*) Program.cs de bunu kullanalım.

        //Şuana kadar Dependency Injection’ı Constructor Injection(Constructor Based Dependecy Injection) yöntemi ile gösterildi.

        //Setter Injection(Setter Based Dependency Injection) yöntemi ile DI’yı uygulamak istiyorsanız eğer sadece “Vasita” sınıfında(yani projenizde DI’yı uyguladığınız sınıfta) aşağıdaki değişikliği yapmanız yeterlidir.

        /*
         *  class Vasita
            {
                public ITasit _tasit { get; set; }
 
                public void Kullan()
                {
                    _tasit.GazVer();
                    _tasit.SagaSinyal();
                    _tasit.FrenYap();
                    _tasit.SolaSinyal();
                }
            }
         */

        /*
         * Yukarıdaki gibi Setter Injection yöntemi uyarlandıysa kullanımı aşağıdaki gibi yapmalısınız.
         *  class Program
            {
                static void Main(string[] args)
                {
                    Vasita vasitaAraba = new Vasita();
                    vasitaAraba._tasit = new Araba();
                    vasitaAraba.Kullan();
        
                    Vasita vasitaOtobus = new Vasita();
                    vasitaOtobus._tasit = new Otobus();
                    vasitaOtobus.Kullan();
        
                    Vasita vasitaMotor = new Vasita();
                    vasitaMotor._tasit = new Motor();
                    vasitaMotor.Kullan();
                }
            }
         */





    }
}
