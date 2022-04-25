using System;
using System.Collections.Generic;
using System.Linq;
using Ciphers_Galore.Model;

namespace Ciphers_Galore
{
    public class Program
    {
        static void Main(string[] args)
        {
            var tool = new Vigenere();
            var results = tool.Decrypt(Console.ReadLine(), "HOCUSPOCUS", true);

            Console.WriteLine();
            Console.WriteLine("Possible Answers:");
            //foreach (var result in results) Console.WriteLine(result);
            foreach (var result in FindMostLikely(results, 25)) Console.WriteLine(result);

            /*var tool = new Vigenere();
            Console.WriteLine(tool.Encrypt(Console.ReadLine(), "DEUS LO VULT", true));*/

            /*string str = "Ifim Siqeawxm Qztamr biw i Ctaqawe siqelsiqaoaim, fxraoaim, otknqimifkwq, imu oxsnzqlt woalmqawq. El biw earefk amdfzlmqaif am qel ulplfxnslmq xd oxsnzqlt woalmol, ntxpauamr i dxtsifaviqaxm xd qel oxmolnqw xd ifrxtaqes imu oxsnzqiqaxm baqe qel Qztamr sioeaml. Qztamr aw baulfk oxmwaultlu qx cl qel diqelt xd oxsnzqlt woalmol imu itqadaoaif amqlffarlmol. Uztamr Bxtfu Bit AA, Qztamr bxtglu dxt qel Rxpltmslmq Oxul imu Oknelt Woexxf (ROOW) iq Cflqoeflk Nitg, Ctaqiam'w oxulctligamr olmqlt. Dxt i qasl, el biw eliu xd Ezq Lareq, qel wloqaxm tlwnxmwacfl dxt Rltsim mipif otknqimifkwaw. El ulpawlu i mzsclt xd qloemahzlw dxt ctligamr Rltsim oaneltw, amofzuamr qel slqexu xd qel cxscl, im lfloqtxsloeimaoif sioeaml qeiq oxzfu damu wlqqamrw dxt qel Lmarsi sioeaml. Idqlt qel bit, el bxtglu iq qel Miqaxmif Nekwaoif Ficxtiqxtk, beltl el otliqlu xml xd qel datwq ulwarmw dxt i wqxtlu-ntxrtis oxsnzqlt, qel IOL. Qeltlidqlt, Qztamr yxamlu Sij Mlbsim'w Oxsnzqamr Ficxtiqxtk iq Simoelwqlt Zmapltwaqk, beltl el iwwawqlu am qel ulplfxnslmq xd qel Simoelwqlt oxsnzqltw imu cloisl amqltlwqlu am siqelsiqaoif caxfxrk. Qztamr'w exsxwljzifaqk tlwzfqlu am i otasamif ntxwlozqaxm belm exsxwljzif ioqw bltl wqaff afflrif am qel Zmaqlu Gamruxs. El ioolnqlu qtliqslmq baqe dlsifl extsxmlw (oelsaoif oiwqtiqaxm) iw im ifqltmiqapl qx ntawxm. Qztamr oxssaqqlu wzaoaul yzwq qbx bllgw wek xd eaw dxtqk-wloxmu catqeuik, dtxs okimaul nxawxmamr. Dadqk klitw fiqlt, dxffxbamr im Amqltmlq oisniarm, Ctaqawe Ntasl Samawqlt Rxtuxm Ctxbm siul im xddaoaif nzcfao inxfxrk xm cleifd xd qel Ctaqawe rxpltmslmq dxt 'qel inniffamr bik el biw qtliqlu.'".ToUpper();

            while (true)
            {
                char old = Console.ReadLine()[0];
                char n = Console.ReadLine()[0];
                str = new Frequency().Replace(str, old, n);
                Console.WriteLine(str);
            } */

        }

        private static List<string> FindMostLikely(List<string> results, int amount)
        {
            return results.OrderByDescending(r => r, new AnswerComparer()).Take(amount).ToList();
        }
    }
}
