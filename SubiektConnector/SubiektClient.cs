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

        public async Task<bool> SubmitOrder(OrderDto order)
        {
            var res = await Task.Run(() =>
            {
                var sub = OpenSubiekt();

                SuDokument dok1 = sub.Dokumenty.Dodaj(SubiektDokumentEnum.gtaSubiektDokumentZK);
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
                    OpenSubiekt();
                }
                catch
                {
                    return false;
                }
                return true;
            });
            return res;
        }


        private Subiekt OpenSubiekt()
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
