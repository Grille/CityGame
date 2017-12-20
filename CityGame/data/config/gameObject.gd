{

 Template
 {
  //0 to 9 Basic data & graphics;
  string name = "-";
  string path = "-";
  string groundPath = "-";
  int buildMode = 0;
  int slopeMode = 0; //0=nothing, 1=onlyStraight, 2=full;
  int diversity = 1;
  int size = 1;
  int groundMode = 0;  //0=nothing, 1=useGraphicMode; 
  int graphicMode = 0;  //0=nothing, 1=self, 2=useArray; 
  int[] graphicNeighbors = [];

  //11 to 14 Simulation data;
  int[] upgradeTyp = [0];
  int[] downgradeTyp = [0];
  int[] demolitionTyp = [0];
  int[] decayTyp = [13];
  int[] destroyTyp = [13];
  int[] canBuiltOn = [0,3,4,5,6,7,8,9,13];       //[typ]

  //effects: 0=not, 1=up, 2=down, 3=demolition, 4=deacy, 5=destroy 6=entf//
  //importance: 0=canNotWork, 1=canWork//
  //15 to 20 Simulation array//
  //AreaPermanent->typ: 0=water, 1=nature, 2=road
  int[,] AreaPermanent = [];      //[[typ,size,value]]
  int[,] AreaDependent = [];      //[[typ,minValue,maxValue,effects,mode]]
  int[,] ResourcesBuild = [];     //[[typ,value,importance]]
  int[,] ResourcesPermanent = []; //[[typ,value,importance]]
  int[,] ResourcesMonthly = [];   //[[typ,value,importance]]
  int[,] ResourcesDependent = []; //[[typ,minValue,maxValue,effects,mode]]
 }

 ID=0; // void
 {
  CanBuiltOn = [];
 }


 // 1 to 20 nature
 ID=1; // water
 {
  name = "Water"; graphicMode = 2; groundMode = 1;graphicNeighbors = [1,2]; groundPath = "../Data/texture/nature/water_0";
  AreaPermanent = [[0,1,1]];
 }
 ID=2; // water
 {
  name = "Saltwater"; graphicMode = 2; groundMode = 1;graphicNeighbors = [1,2]; groundPath = "../Data/texture/nature/saltwater_0";
  AreaPermanent = [[0,1,1]]
 }

 ID=3; // forest Conifer
 {
  name = "Conifer"; diversity = 5; path = "../Data/texture/nature/Conifer";buildMode = 1;
  canBuiltOn = [0,4,5];
  downgradeTyp = [0];
  AreaPermanent = [[1,3,2]];
  AreaDependent = [[1,0,10000,2 ,1]];
 }
 ID=4; // forest Deciduous
 {
  name = "Deciduous"; diversity = 5; path = "../Data/texture/nature/Deciduous";buildMode = 1;
  canBuiltOn = [0,3,5];
  downgradeTyp = [0];
  AreaPermanent = [[1,3,2]];
  AreaDependent = [[1,0,10000,2 ,1]];
 }
 ID=5; // forest Palm
 {
  name = "Palm"; diversity = 5; path = "../Data/texture/nature/Palm";buildMode = 1;
  canBuiltOn = [0,3,4];
  downgradeTyp = [0];
  AreaPermanent = [[1,3,2]];
  AreaDependent = [[1,0,10000,2 ,1]];
 }

 ID=6; // forest
 {
  name = "Conifer dead"; diversity = 5; path = "../Data/texture/nature/ConiferDead";
 }
 ID=7; // forest
 {
  name = "Oak dead"; diversity = 5; path = "../Data/texture/nature/OakDead";
 }
 ID=8; // forest
 {
  name = "Palm dead"; diversity = 5; path = "../Data/texture/nature/PalmDead";
 }

 ID=9; // destroyed forest
 {
  name = "Destroyed forest"; diversity = 5; path = "../Data/texture/nature/DestroyedForest";
 }

 ID=10; // void
 {
  name = "Stone Small";
 }
 ID=11; // void
 {
  name = "Stone large";
 }
 ID=12; // void
 {
  name = "Garbage";
 }
 ID=13; // void
 {
  name = "debris"; diversity = 5; path = "../Data/texture/urban/debris/debris";CanBuiltOn = [];
 }
 ID=14; // void
 {
 }
 ID=15; // void
 {
 }
 ID=16; // void
 {
 }
 ID=17; // void
 {
 }
 ID=18; // void
 {
 }
 ID=19; // void
 {
 }
 ID=20; // void
 {
 }

 //traffic 21 to 40

 ID=21; // 
 {
  name = "dirt way"; 
  canBuiltOn = [0,3,4,5];
  graphicMode = 2; groundMode = 1; graphicNeighbors = [21,22,23,24]; groundPath = "../Data/texture/urban/road/FW1_2";
  AreaPermanent = [[2,1,1]];
  buildMode = 1;
 }
 ID=22; //
 {
  name = "small road"; 
  canBuiltOn = [0,3,4,5,21];
  graphicMode = 2; groundMode = 1; graphicNeighbors = [21,22,23,24]; groundPath = "../Data/texture/urban/road/RS_2";
  AreaPermanent = [[2,1,1],[2,1,100]];
  buildMode = 1;
 }
 ID=23; // 
 {
  name = "medium road"; 
  canBuiltOn = [0,3,4,5,21,22];
  graphicMode = 2; groundMode = 1; graphicNeighbors = [21,22,23,24]; groundPath = "../Data/texture/urban/road/RM_1";
  AreaPermanent = [[2,2,1],[2,1,100]];
  buildMode = 1;
 }
 ID=24; // 
 {
  name = "large road"; 
  canBuiltOn = [0,3,4,5,21,22,23];
  graphicMode = 2; groundMode = 1; graphicNeighbors = [21,22,23,24]; groundPath = "../Data/texture/urban/road/RL_1";
  AreaPermanent = [[2,3,1],[2,1,100]];
  buildMode = 1;
 }
 ID=25; // 
 {
 }
 ID=26; // void
 {
 }
 ID=27; // void
 {
 }
 ID=28; // void
 {
 }
 ID=29; // void
 {
 }
 ID=30; // void
 {
 }
 ID=31; // void
 {
 }
 ID=32; // void
 {
 }
 ID=33; // void
 {
 }
 ID=34; // void
 {
 }
 ID=35; // void
 {
 }
 ID=36; // void
 {
 }
 ID=37; // void
 {
 }
 ID=38; // void
 {
 }
 ID=39; // void
 {
 }
 ID=40; // void
 {
 }
 // supply 41 to 60

 ID=41; // coal power plant
 {
  name = "coal power plant"; size = 3; path = "../Data/texture/urban/power/KKW";
  AreaPermanent = [[1,8,-15],[1,12,-15],[1,16,-15]];
  AreaDependent = [[2,100,10000,4, 1]];
 }
 ID=42; // gas power plant
 {
  name = "gas power plant"; size = 3; path = "../Data/texture/urban/power/GKW";
  AreaPermanent = [[1,10,-15]];
  AreaDependent = [[2,100,10000,4, 1]];
 }
 ID=43; // nuclear power plant
 {
  name = "nuclear power plant"; size = 3; path = "../Data/texture/urban/power/AKW";
  AreaDependent = [[2,100,10000,4, 1]];
 }
 ID=44; // 
 {
  name = "solar power plant"; size = 3; path = "../Data/texture/urban/power/SKW";
  AreaDependent = [[2,100,10000,4, 1]];
 }
 ID=45; // 
 {
  name = "wind power plant"; size = 2; path = "../Data/texture/urban/power/WKW";
 }
 ID=46; // void
 {
 }
 ID=47; // void
 {
 }
 ID=48; // void
 {
 }
 ID=49; // void
 {
 }
 ID=50; // void
 {
 }
 ID=51; // void
 {
  name = "water pump";path = "../Data/texture/urban/water/WP";
  AreaDependent = [[0,1,10000,5, 1],[1,0,10000,2 ,1]];
  downgradeTyp = [52];
 }
 ID=52; // void
 {
  name = "polluted water pump";path = "../Data/texture/urban/water/WPp";
  upgradeTyp = [51];
 }
 ID=53; // void
 {
  name = "water tower";path = "../Data/texture/urban/water/WT";
  AreaDependent = [[1,0,10000,2 ,1]];
  downgradeTyp = [54];
 }
 ID=54; // void
 {
  name = "polluted water tower";path = "../Data/texture/urban/water/WTp";
  upgradeTyp = [53];
 }
 ID=55; // void
 {
  name = "sewage plant"; size = 3; path = "../Data/texture/urban/water/KW";
  AreaDependent = [[2,100,10000,5, 1]];
 }
 ID=56; // void
 {
 }
 ID=57;
 {
 }
 ID=58;
 {
  name = "landfill"; groundPath = "../Data/texture/urban/disposal/MD";
 }
 ID=59;
 {
  name = "filed landfill";graphicMode = 1; groundMode = 1;groundPath = "../Data/texture/urban/disposal/MDF";diversity = 5; path = "../Data/texture/urban/disposal/rubbish";
  AreaPermanent = [[1,5,-3]];
 }
 ID=60;
 {
  name = "incinerator";size = 2;path = "../Data/texture/urban/disposal/MV";
  AreaDependent = [[2,100,10000,5, 1]];
  AreaPermanent = [[1,8,-15],[1,12,-15]];
 }


 // zones 61 to 90

 ID=61; // Residential
 {
  name = "Residential"; path = "../Data/texture/urban/Residential/W0";diversity = 5;
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=62;
 {
  name = "Residential"; path = "../Data/texture/urban/Residential/W1";diversity = 5;
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=63;
 {
  name = "Residential"; path = "../Data/texture/urban/Residential/W2";diversity = 5;
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=64;
 {
 }
 ID=65;
 {
 }
 ID=66;
 {
 }
 ID=67;
 {
 }
 ID=68;
 {
 }
 ID=69;
 {
 }
 ID=70;
 {
 }
 ID=71;
 {
  name = "Comercial"; path = "../Data/texture/urban/Comercial/G0";diversity = 5;
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=72;
 {
  name = "Comercial"; path = "../Data/texture/urban/Comercial/G1";diversity = 5;
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=73;
 {
 }
 ID=74;
 {
 }
 ID=75;
 {
  name = "Comercial"; path = "../Data/texture/urban/Comercial/G4";diversity = 5;
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=76;
 {
 }
 ID=77;
 {
 }
 ID=78;
 {
 }
 ID=79;
 {
 }
 ID=80;
 {
 }
 ID=81;
 {
  name = "Industrieal"; path = "../Data/texture/urban/Industrieal/B0";diversity = 6;
 }
 ID=82;
 {
 }
 ID=83; 
 {
  name = "Industrieal"; path = "../Data/texture/urban/Industrieal/I0";diversity = 5;
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=84;
 {

  name = "Industrieal"; path = "../Data/texture/urban/Industrieal/I1";diversity = 5;
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=85;
 {
 }
 ID=86;
 {
 }
 ID=87;
 {
  name = "Industrieal"; path = "../Data/texture/urban/Industrieal/I2a";diversity = 5;
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=88; 
 {
 }
 ID=89;
 {
 }
 ID=90;
 {
 }

 // public 91 to 110


 ID=91;
 {
  name = "small fire department"; size = 2; path = "../Data/texture/urban/public/FW1";
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=92;
 {
  name = "large fire department"; size = 3; path = "../Data/texture/urban/public/FW2";
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=93;
 {
  name = "small police department"; size = 2; path = "../Data/texture/urban/public/PW1";
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=94;
 {
  name = "large police departmen"; size = 3; path = "../Data/texture/urban/public/PW2";
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=95;
 {
  name = "Comercial"; size = 3; path = "../Data/texture/urban/public/G";
  AreaDependent = [[2,1,10000,5, 1]];
  buildMode = 1;
 }
 ID=96;
 {
 }
 ID=97;
 {
 }
 ID=98;
 {
 }
 ID=99;
 {
 }
 ID=100;
 {
 }
 ID=101;
 {
 }
 ID=102;
 {
  name = "hospital"; size = 3; path = "../Data/texture/urban/public/KH";
 }
 ID=103;
 {
 }
 ID=104;
 {
 }
 ID=105;
 {
 }
 ID=106;
 {
 }
 ID=107;
 {
 }
 ID=108;
 {
 }
 ID=109;
 {
 }
 ID=110;
 {
 }

}