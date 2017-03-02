using ExtentionMethods;
using NUnit.Framework;
using Scrabble;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Scrabble.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        //#region //WordFind
        //[Test]
        //[Category("WordFind")]
        //public void WordFind_NoAnchors_1Word()
        //{
        //    //Arrange

        //    Game game = new Game("EIUQSZZ"); 


        //    //Act
        //    List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 7);

        //    //Assert
        //    Assert.That(result.Count, Is.EqualTo(1));
        //    Assert.That(result[0], Is.EqualTo("QUIZZES"));
        //}

        //[Test]
        //[Category("WordFind")]
        //public void WordFind_WithAnchors_1Word()
        //{
        //    //Arrange

        //    Game game = new Game("EIUZZ");


        //    //Act
        //    List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 7, new List<Tuple<int, char>> { Tuple.Create(0, 'Q'), Tuple.Create(6, 'S') });

        //    //Assert
        //    Assert.That(result.Count, Is.EqualTo(1));
        //    Assert.That(result[0], Is.EqualTo("QUIZZES"));
        //}

        //[Test]
        //[Category("WordFind")]
        //public void WordFind_WithAnchors_2Word()
        //{
        //    //Arrange

        //    Game game = new Game("AR");


        //    //Act
        //    List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 4, new List<Tuple<int, char>> { Tuple.Create(0, 'W'), Tuple.Create(3, 'P') });

        //    //Assert
        //    Assert.That(result.Count, Is.EqualTo(2));
        //    Assert.That(result, Is.EquivalentTo(new List<string> { "WRAP", "WARP" }));
        //}

        //[Test]
        //[Category("WordFind")]
        //public void WordFind_Unused_Blank()
        //{
        //    //Arrange

        //    Game game = new Game("AI?");


        //    //Act
        //    List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 3, new List<Tuple<int, char>> { Tuple.Create(0, 'Z'), Tuple.Create(2, 'G') });
            
        //    //Assert
        //    Assert.That(result.Count, Is.EqualTo(2));
        //    Assert.That(result, Is.EquivalentTo(new List<string> { "ZAG", "ZIG" }));
        //}

        //[Test]
        //[Category("WordFind")]
        //public void WordFind_Two_Blanks()
        //{
        //    //Arrange

        //    Game game = new Game("Q??");


        //    //Act
        //    List<string> result1 = game.GetDictionary().WordFind(game.GetTrayString(), 3);
        //    List<string> result2 = game.GetDictionary().WordFind(game.GetTrayString(), 2);
        //    List<string> result3 = game.GetDictionary().WordFind(game.GetTrayString(), 3, new List<Tuple<int, char>> {Tuple.Create(2, 'Q') });

        //    //Assert
        //    Assert.That(result1, Is.EquivalentTo(new List<string> { "QAT", "QIS", "QUA", "SUQ" }));
        //    Assert.That(result2.Count, Is.EqualTo(101));
        //    Assert.That(result3, Is.EquivalentTo(new List<string> { "SUQ" }));
        //}

        //[Test]
        //[Category("WordFind")]
        //public void WordFind_Three_Blanks_Only()
        //{
        //    //Arrange

        //    Game game = new Game("???");


        //    List<string> expected = new List<string>
        //    {
        //        #region
        //                                    "AAH","AAL","AAS","ABA","ABO","ABS","ABY","ACE","ACT","ADD","ADO","ADS","ADZ","AFF","AFT","AGA","AGE","AGO","AGS","AHA","AHI","AHS","AID","AIL","AIM","AIN","AIR","AIS","AIT","ALA","ALB","ALE","ALL","ALP","ALS","ALT","AMA","AMI","AMP","AMU","ANA","AND","ANE","ANI","ANT","ANY","APE","APO","APP","APT","ARB","ARC","ARE","ARF","ARK","ARM","ARS","ART","ASH","ASK","ASP","ASS","ATE","ATT","AUK","AVA","AVE","AVO","AWA","AWE","AWL","AWN","AXE","AYE","AYS","AZO","BAA","BAD","BAG","BAH","BAL","BAM","BAN","BAP","BAR","BAS","BAT","BAY","BED","BEE","BEG","BEL","BEN","BES","BET","BEY","BIB","BID","BIG","BIN","BIO","BIS","BIT","BIZ","BOA","BOB","BOD","BOG","BOO","BOP","BOS","BOT","BOW","BOX","BOY","BRA","BRO","BRR","BUB","BUD","BUG","BUM","BUN","BUR","BUS","BUT","BUY","BYE","BYS","CAB","CAD","CAM","CAN","CAP","CAR","CAT","CAW","CAY","CEE","CEL","CEP","CHI","CIG","CIS","COB","COD","COG","COL","CON","COO","COP","COR","COS","COT","COW","COX","COY","COZ","CRU","CRY","CUB","CUD","CUE","CUM","CUP","CUR","CUT","CWM","DAB","DAD","DAG","DAH","DAK","DAL","DAM","DAN","DAP","DAW","DAY","DEB","DEE","DEF","DEL","DEN","DEV","DEW","DEX","DEY","DIB","DID","DIE","DIF","DIG","DIM","DIN","DIP","DIS","DIT","DOC","DOE","DOG","DOL","DOM","DON","DOR","DOS","DOT","DOW","DRY","DUB","DUD","DUE","DUG","DUH","DUI","DUN","DUO","DUP","DYE","EAR","EAT","EAU","EBB","ECU","EDH","EDS","EEK","EEL","EFF","EFS","EFT","EGG","EGO","EKE","ELD","ELF","ELK","ELL","ELM","ELS","EME","EMS","EMU","END","ENG","ENS","EON","ERA","ERE","ERG","ERN","ERR","ERS","ESS","ETA","ETH","EVE","EWE","EYE","FAB","FAD","FAG","FAN","FAR","FAS","FAT","FAX","FAY","FED","FEE","FEH","FEM","FEN","FER","FES","FET","FEU","FEW","FEY","FEZ","FIB","FID","FIE","FIG","FIL","FIN","FIR","FIT","FIX","FIZ","FLU","FLY","FOB","FOE","FOG","FOH","FON","FOP","FOR","FOU","FOX","FOY","FRO","FRY","FUB","FUD","FUG","FUN","FUR","GAB","GAD","GAE","GAG","GAL","GAM","GAN","GAP","GAR","GAS","GAT","GAY","GED","GEE","GEL","GEM","GEN","GET","GEY","GHI","GIB","GID","GIE","GIG","GIN","GIP","GIT","GNU","GOA","GOB","GOD","GOO","GOR","GOS","GOT","GOX","GOY","GUL","GUM","GUN","GUT","GUV","GUY","GYM","GYP","HAD","HAE","HAG","HAH","HAJ","HAM","HAO","HAP","HAS","HAT","HAW","HAY","HEH","HEM","HEN","HEP","HER","HES","HET","HEW","HEX","HEY","HIC","HID","HIE","HIM","HIN","HIP","HIS","HIT","HMM","HOB","HOD","HOE","HOG","HON","HOP","HOS","HOT","HOW","HOY","HUB","HUE","HUG","HUH","HUM","HUN","HUP","HUT","HYP","ICE","ICH","ICK","ICY","IDS","IFF","IFS","IGG","ILK","ILL","IMP","INK","INN","INS","ION","IRE","IRK","ISM","ITS","IVY","JAB","JAG","JAM","JAR","JAW","JAY","JEE","JET","JEU","JEW","JIB","JIG","JIN","JOB","JOE","JOG","JOT","JOW","JOY","JUG","JUN","JUS","JUT","KAB","KAE","KAF","KAS","KAT","KAY","KEA","KEF","KEG","KEN","KEP","KEX","KEY","KHI","KID","KIF","KIN","KIP","KIR","KIS","KIT","KOA","KOB","KOI","KOP","KOR","KOS","KUE","KYE","LAB","LAC","LAD","LAG","LAM","LAP","LAR","LAS","LAT","LAV","LAW","LAX","LAY","LEA","LED","LEE","LEG","LEI","LEK","LES","LET","LEU","LEV","LEX","LEY","LEZ","LIB","LID","LIE","LIN","LIP","LIS","LIT","LOB","LOG","LOO","LOP","LOT","LOW","LOX","LUG","LUM","LUV","LUX","LYE","MAC","MAD","MAE","MAG","MAN","MAP","MAR","MAS","MAT","MAW","MAX","MAY","MED","MEG","MEL","MEM","MEN","MET","MEW","MHO","MIB","MIC","MID","MIG","MIL","MIM","MIR","MIS","MIX","MOA","MOB","MOC","MOD","MOG","MOL","MOM","MON","MOO","MOP","MOR","MOS","MOT","MOW","MUD","MUG","MUM","MUN","MUS","MUT","MYC","NAB","NAE","NAG","NAH","NAM","NAN","NAP","NAW","NAY","NEB","NEE","NEG","NET","NEW","NIB","NIL","NIM","NIP","NIT","NIX","NOB","NOD","NOG","NOH","NOM","NOO","NOR","NOS","NOT","NOW","NTH","NUB","NUN","NUS","NUT","OAF","OAK","OAR","OAT","OBA","OBE","OBI","OCA","ODA","ODD","ODE","ODS","OES","OFF","OFT","OHM","OHO","OHS","OIL","OKA","OKE","OLD","OLE","OMS","ONE","ONO","ONS","OOH","OOT","OPE","OPS","OPT","ORA","ORB","ORC","ORE","ORS","ORT","OSE","OUD","OUR","OUT","OVA","OWE","OWL","OWN","OXO","OXY","PAC","PAD","PAH","PAL","PAM","PAN","PAP","PAR","PAS","PAT","PAW","PAX","PAY","PEA","PEC","PED","PEE","PEG","PEH","PEN","PEP","PER","PES","PET","PEW","PHI","PHT","PIA","PIC","PIE","PIG","PIN","PIP","PIS","PIT","PIU","PIX","PLY","POD","POH","POI","POL","POM","POO","POP","POT","POW","POX","PRO","PRY","PSI","PST","PUB","PUD","PUG","PUL","PUN","PUP","PUR","PUS","PUT","PYA","PYE","PYX","QAT","QIS","QUA","RAD","RAG","RAH","RAI","RAJ","RAM","RAN","RAP","RAS","RAT","RAW","RAX","RAY","REB","REC","RED","REE","REF","REG","REI","REM","REP","RES","RET","REV","REX","RHO","RIA","RIB","RID","RIF","RIG","RIM","RIN","RIP","ROB","ROC","ROD","ROE","ROM","ROT","ROW","RUB","RUE","RUG","RUM","RUN","RUT","RYA","RYE","SAB","SAC","SAD","SAE","SAG","SAL","SAP","SAT","SAU","SAW","SAX","SAY","SEA","SEC","SEE","SEG","SEI","SEL","SEN","SER","SET","SEW","SEX","SHA","SHE","SHH","SHY","SIB","SIC","SIM","SIN","SIP","SIR","SIS","SIT","SIX","SKA","SKI","SKY","SLY","SOB","SOD","SOL","SOM","SON","SOP","SOS","SOT","SOU","SOW","SOX","SOY","SPA","SPY","SRI","STY","SUB","SUE","SUK","SUM","SUN","SUP","SUQ","SYN","TAB","TAD","TAE","TAG","TAJ","TAM","TAN","TAO","TAP","TAR","TAS","TAT","TAU","TAV","TAW","TAX","TEA","TED","TEE","TEG","TEL","TEN","TET","TEW","THE","THO","THY","TIC","TIE","TIL","TIN","TIP","TIS","TIT","TOD","TOE","TOG","TOM","TON","TOO","TOP","TOR","TOT","TOW","TOY","TRY","TSK","TUB","TUG","TUI","TUN","TUP","TUT","TUX","TWA","TWO","TYE","UDO","UGH","UKE","ULU","UMM","UMP","UNS","UPO","UPS","URB","URD","URN","URP","USE","UTA","UTE","UTS","VAC","VAN","VAR","VAS","VAT","VAU","VAV","VAW","VEE","VEG","VET","VEX","VIA","VID","VIE","VIG","VIM","VIS","VOE","VOW","VOX","VUG","VUM","WAB","WAD","WAE","WAG","WAN","WAP","WAR","WAS","WAT","WAW","WAX","WAY","WEB","WED","WEE","WEN","WET","WHA","WHO","WHY","WIG","WIN","WIS","WIT","WIZ","WOE","WOG","WOK","WON","WOO","WOP","WOS","WOT","WOW","WRY","WUD","WYE","WYN","XIS","YAG","YAH","YAK","YAM","YAP","YAR","YAW","YAY","YEA","YEH","YEN","YEP","YES","YET","YEW","YID","YIN","YIP","YOB","YOD","YOK","YOM","YON","YOU","YOW","YUK","YUM","YUP","ZAG","ZAP","ZAS","ZAX","ZED","ZEE","ZEK","ZEP","ZIG","ZIN","ZIP","ZIT","ZOA","ZOO","ZUZ", "ZZZ"
        //        #endregion
        //    };
        //    //Act
        //    List<string> result = game.GetDictionary().WordFind(game.GetTrayString(), 3);

        //    //Assert
        //    Assert.That(result.Count, Is.EqualTo(expected.Count));
        //    Assert.That(result, Is.EquivalentTo(expected));
        //}
        //#endregion

        #region //WildCardCombinations
        [Test]
        [Category("WildCardCombinations")]
        public void WildCardCombinations_NoWildCards_EqualLetters()
        {

            //Arrange
            string targetString = "ABC";
            string availableString = "ABC";

            //Act
            List<string> resultList = targetString.WildCardCombinations(availableString);

            
            //Assert
            Assert.That(resultList.Count, Is.EqualTo(1));
            Assert.That(resultList[0], Is.EqualTo("ABC"));
        }

        [Test]
        [Category("WildCardCombinations")]
        public void WildCardCombinations_NoWildCards_ExtraLetters()
        {

            //Arrange
            string targetString = "ABC";
            string availableString = "ABCABC";

            //Act
            List<string> resultList = targetString.WildCardCombinations(availableString);


            //Assert
            Assert.That(resultList.Count, Is.EqualTo(1));
            Assert.That(resultList[0], Is.EqualTo("ABC"));
        }

        [Test]
        [Category("WildCardCombinations")]
        public void WildCardCombinations_1WildCard_NoExtra()
        {

            //Arrange
            string targetString = "ABC";
            string availableString = "AB?";

            //Act
            List<string> resultList = targetString.WildCardCombinations(availableString);


            //Assert
            Assert.That(resultList.Count, Is.EqualTo(1));
            Assert.That(resultList[0], Is.EqualTo("AB?"));
        }

        [Test]
        [Category("WildCardCombinations")]
        public void WildCardCombinations_2WildCard_NoExtra()
        {

            //Arrange
            string targetString = "ABC";
            string availableString = "B??";

            //Act
            List<string> resultList = targetString.WildCardCombinations(availableString);


            //Assert
            Assert.That(resultList.Count, Is.EqualTo(1));
            Assert.That(resultList[0], Is.EqualTo("?B?"));
        }

        [Test]
        [Category("WildCardCombinations")]
        public void WildCardCombinations_1WildCard_1Extra()
        {

            //Arrange
            string targetString = "ABC";
            string availableString = "CAB?";

            List<string> expectedList = new List<string>
            {
                "ABC",
                "AB?",
                "A?C",
                "?BC"
            };
            //Act
            List<string> resultList = targetString.WildCardCombinations(availableString);


            //Assert
            Assert.That(resultList, Is.EquivalentTo(expectedList));           

        }

        [Test]
        [Category("WildCardCombinations")]
        public void WildCardCombinations_1WildCard_LotsExtra()
        {

            //Arrange
            string targetString = "ABC";
            string availableString = "CAB?ABC";

            List<string> expectedList = new List<string>
            {
                "ABC",
                "AB?",
                "A?C",
                "?BC"
            };
            //Act
            List<string> resultList = targetString.WildCardCombinations(availableString);


            //Assert
            Assert.That(resultList, Is.EquivalentTo(expectedList));

        }

        [Test]
        [Category("WildCardCombinations")]
        public void WildCardCombinations_1ExtraWildCard_RepeatedLetters()
        {

            //Arrange
            string targetString = "ABCABC";
            string availableString = "CAB?ABC";

            List<string> expectedList = new List<string>
            {
                "ABCABC",
                "?BCABC",
                "A?CABC",
                "AB?ABC",
                "ABC?BC",
                "ABCA?C",
                "ABCAB?"
            };
            //Act
            List<string> resultList = targetString.WildCardCombinations(availableString);


            //Assert
            Assert.That(resultList, Is.EquivalentTo(expectedList));

        }

        [Test]
        [Category("WildCardCombinations")]
        public void WildCardCombinations_1WildCard_RepeatedLetters()
        {

            //Arrange
            string targetString = "ABBA";
            string availableString = "ABB?";

            List<string> expectedList = new List<string>
            {
                "ABB?",
                "?BBA"
            };
            //Act
            List<string> resultList = targetString.WildCardCombinations(availableString);


            //Assert
            Assert.That(resultList, Is.EquivalentTo(expectedList));

        }

        [Test]
        [Category("WildCardCombinations")]
        public void WildCardCombinations_2WildCard_2RepeatedLetters()
        {

            //Arrange
            string targetString = "ABBA";
            string availableString = "AB??";

            List<string> expectedList = new List<string>
            {       
                "??BA",
                "?B?A",
                "A?B?",
                "AB??",
            };
            //Act
            List<string> resultList = targetString.WildCardCombinations(availableString);


            //Assert
            Assert.That(resultList, Is.EquivalentTo(expectedList));

        }

        [Test]
        [Category("WildCardCombinations")]
        public void WildCardCombinations_ManyWildCards_ManyRepeatedLetters()
        {

            //Arrange
            string targetString = "ABBBCCC";
            string availableString = "BBBBCCC???????";

            //Act
            List<string> resultList = targetString.WildCardCombinations(availableString);


            //Assert
            Assert.That(resultList.Count, Is.EqualTo(64));
        }
        #endregion
    }
}