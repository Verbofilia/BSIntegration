using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Common.Dto;
using Common.Interfaces;
using InsERT;

namespace SubiektConnector
{
    public class SubiektClient:ITargetConnector
    {

        private readonly Subiekt _subiekt;

        public SubiektClient()
        {
            //requires some work to add lazy initialization for better user expierience 
            //not that straight forward due to STA limitations
            _subiekt = OpenSubiekt();
        }
        
        public async Task<bool> SubmitOrder(OrderDto order)
        {
            var res = await Task.Run(() =>
            {

                SuDokument dok1 = _subiekt.Dokumenty.Dodaj(SubiektDokumentEnum.gtaSubiektDokumentZK);
                dok1.KontrahentId = 1;

                //added to artificially change products due to missing external id,
                int counter = 1;
                foreach (var p in order.Products)
                {
                    SuPozycja oPoz = dok1.Pozycje.Dodaj(counter++);
                    oPoz.IloscJm = p.Quantity;
                    oPoz.Jm = "szt.";
                    oPoz.CenaBruttoPoRabacie = p.PriceBrutto;
                }
                dok1.Zapisz();
                return true;
            });
            return res;
        }

        public async Task<bool> TestConnection()
        {
            var res = await Task.Run(() =>
            {
                try
                {
                    var s = _subiekt;
                    var v = s.Wersja;
                    if (string.IsNullOrEmpty(v))
                        return false;
                }
                catch
                {
                    return false;
                }
                return true;
            });
            return res;
        }


        private static Subiekt OpenSubiekt()
        {
            //var dodatki = new Dodatki();
            var gt = new GT();
            gt.Produkt = ProduktEnum.gtaProduktSubiekt;

            gt.Autentykacja = AutentykacjaEnum.gtaAutentykacjaWindows; // AutentykacjaEnum.gtaAutentykacjaMieszana
            gt.Serwer = @"(local)\InsERTGT";
            gt.Uzytkownik = "";
            //     gt.UzytkownikHaslo = dodatki.Szyfruj("");

            gt.Baza = "";
            gt.Operator = "";
            //    gt.OperatorHaslo = dodatki.Szyfruj("");

            var subGT = gt.Uruchom((int)UruchomDopasujEnum.gtaUruchomDopasujOperatora, (int)UruchomEnum.gtaUruchom);
            var sub = subGT as Subiekt;
            if (sub == null)
            {
                throw new InvalidOperationException();
            }
            return sub;
        }

    }
}
