
 Attributes
 {
  //Basic;
  string name;
  byte buildMode; //0 single,1 brush,2 rnd_line,3 con_line,4 equal_line,5 rnd_area, 6 equal_area
  byte[] repalceTyp; //[[tileTyp,replaceTyp]]

  //Graphic
  string structPath, groundPath;
  byte groundMode, structMode; //0:(else),1:(all),2:(straight,fork,ends,else),3:(straight,curves,fork,else),4:(straight,curves,ends,else), 5:(straight,fork,else),6:(straight,curves,else),7:(straight,ends,else),8:(straight,else)
  
  //mode{not,all,focu,foen,cuen,fo,cu,en,st}
  
  byte[] structNeighbors, groundNeighbors;
  byte size;

  //Simulation 1;
  byte[] upgradeTyp,downgradeTyp,demolitionTyp,decayTyp,destroyTyp;
  byte[] canBuiltOn;

  //Simulation 2;
  int[] AreaPermanent;      //[[typ,size,value]]
  int[] AreaDependent;      //[[typ,minValue,maxValue,effects,mode]]
  int[] ResourcesEffect;    //[[firedEffect,typ,value]]
  int[] ResourcesBuild;     //[[typ,value]]
  int[] ResourcesPermanent; //[[typ,value]]
  int[] ResourcesMonthly;   //[[typ,value]]
  int[] ResourcesDependent; //[[typ,minValue,maxValue,effects,mode]]
 }

 Init{

  buildMode = 0;
  repalceTyp = [];

  groundMode = 0;structMode = 0;
  groundNeighbors = [];
  structNeighbors = [];

  demolitionTyp = [0];
  size = 1;
  decayTyp = [13];
  destroyTyp = [13];
  canBuiltOn = [0,3 to 9,13];

}

 <0> {
  canBuiltOn = [];

  ResourcesDependent = [[res.waste,1000,i.max,effect.down,0]];
  ResourcesEffect = [[effect.down,res.waste,-100]];
  downgradeTyp = [12];
 }
 


 // 1 to 20 nature
 <1> {
  name = "Water"; 
  groundMode = effect.up;
  groundNeighbors = [1,2,25 to 28]; 
  groundPath = "../Data/texture/nature/water_0";

  AreaPermanent = [area.water,1,1];
 }
 <2>:1 {
  name = "Saltwater"; 
  groundPath = "../Data/texture/nature/saltwater_0";

  AreaPermanent = [area.saltwater,1,1]
 }

 //	<---- Forest ---->
 <3> {
  
  name = "Conifer";  
  structPath = "../Data/texture/nature/Conifer";
  buildMode = 1;
  canBuiltOn = [0];
  

  downgradeTyp = [6];
  destroyTyp = [9];
  
  AreaPermanent = [[area.pollution,6,1]];
  
  AreaDependent = [[area.pollution,0, i.max ,effect.down ,1]];
 
 }
 <4>:3 {
  name = "Deciduous"; 
  structPath = "../Data/texture/nature/Deciduous";
  downgradeTyp = [7];
 }
 <5>:3 {
  name = "Palm"; 
  structPath = "../Data/texture/nature/Palm";
  downgradeTyp = [8];
 }

 <6> {
  name = "Conifer dead"; 
  structPath = "../Data/texture/nature/ConiferDead";
  upgradeTyp = [3];
  destroyTyp = [9];
  AreaPermanent = [[area.pollution,5,-1]];
  AreaDependent = [[area.pollution,0,i.max ,effect.up ,0]];
 }
 <7>:6 {
  name = "Oak dead"; 
  structPath = "../Data/texture/nature/DeciduousDead";
  upgradeTyp = [4];
 }
 <8>:6 {
  name = "Palm dead"; 
  structPath = "../Data/texture/nature/PalmDead";
  upgradeTyp = [5];
 }

 <9> {
  name = "Destroyed forest"; 
  structPath = "../Data/texture/nature/DestroyedForest";
 }


 <10> {
  name = "Stone Small"; structPath = "../Data/texture/nature/StoneSmall";
 }
 <11> {
  name = "Stone large";
 }

 <12> {
  name = "Garbage";structPath = "../Data/texture/urban/disposal/rubbish";canBuiltOn = [0];
  AreaPermanent = [[area.pollution,10,-4]];
  ResourcesDependent = [[res.waste,i.min,800,effect.up,0]];
  ResourcesEffect = [[effect.up,res.waste,+100]];
  ResourcesMonthly = [res.waste,-10];
  upgradeTyp = [0];
 }
 <13> {
  name = "debris"; structPath = "../Data/texture/urban/debris/debris";canBuiltOn = [];
 }

//	<---- Road ---->

 <21> {
  name = "dirt way"; 
  buildMode = 4;
  repalceTyp = [1,25];

  groundMode = gmode.all; 
  groundNeighbors = [21 to 28]; 
  groundPath = "../Data/texture/urban/road/FW1_2";
  AreaPermanent = [area.road,1,100]; //typ,size,value
  ResourcesBuild = [res.money,-8];
  ResourcesMonthly = [res.money,-2];
 }

 <23>:21 {
  name = "medium road"; 
  repalceTyp = [1,26];

  canBuiltOn + [21,22];
  groundPath = "../Data/texture/urban/road/RM_1";
  AreaPermanent + [area.road,2,1];
  ResourcesBuild = [res.money,-16];
  ResourcesMonthly = [res.money,-4];
 }
 <24>:23 {
  name = "large road"; 
  canBuiltOn + [23];
  groundPath = "../Data/texture/urban/road/RL_1";
  AreaPermanent + [area.road,4,1];
  //ResourcesBuild = [res.money,-24];
  ResourcesMonthly = [res.money,-8];
 }
 <25>:1 {
  demolitionTyp = [1];
  decayTyp = [1];
  destroyTyp = [1];
  name = "wood bridge"; 
  canBuiltOn = [1];
  structMode = gmode.st;
  structNeighbors = [21 to 24,25]; 
  structPath = "../Data/texture/urban/road/BW";
 }
 <26>:25 {
  name = "medium bridge"; 
  structNeighbors = [21 to 24,26]; 
  structPath = "../Data/texture/urban/road/BM";
 }
 <27>:25 {
  name = "large bridge"; 
  structNeighbors = [21 to 24,27]; 
  structPath = "../Data/texture/urban/road/BL";
 }
 <28>:2 {
  demolitionTyp = [2];
  decayTyp = [2];
  destroyTyp = [2];
  name = "large bridge"; 
  canBuiltOn = [2];
  structMode = gmode.st;
  structNeighbors = [21 to 24,28]; 
  structPath = "../Data/texture/urban/road/BL";
 }

 // supply 41 to 60

 <41> {
  name = "coal power plant";size = 3; 
  structPath = "../Data/texture/urban/power/KKW";

  AreaPermanent = [[area.pollution,16,-45]];
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,5000]];
  ResourcesBuild = [[res.money,-5000]];
  ResourcesMonthly = [res.waste,100];
 }
 <42> {
  name = "gas power plant"; size = 3; 
  structPath = "../Data/texture/urban/power/GKW";

  AreaPermanent = [[area.pollution,10,-15]];
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,3000]];
  ResourcesBuild = [[res.money,-4500]];
  ResourcesMonthly = [[res.money,-10],[res.waste,50]];
 }
 <43> {
  name = "nuclear power plant"; size = 3; 
  structPath = "../Data/texture/urban/power/AKW";

  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,15000]];
  ResourcesBuild = [[res.money,-20000]];
  ResourcesMonthly = [[res.money,-10],[res.waste,1000]];
 }
 <44> {
  name = "solar power plant"; size = 3; 
  structPath = "../Data/texture/urban/power/SKW";

  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,2000]];
  ResourcesBuild = [[res.money,-15000]];
  ResourcesMonthly = [[res.money,-10]];
 }
 <45> {
  name = "wind power plant"; size = 1; 
  structPath = "../Data/texture/urban/power/WKW";

  ResourcesPermanent = [[res.energy,600]];
  ResourcesBuild = [[res.money,-2500]];
  ResourcesMonthly = [[res.money,-10]];
 }

 <51> {
  name = "water pump";structPath = "../Data/texture/urban/water/WP";
  AreaDependent = [[area.water,1,i.max,5, 1],[area.pollution,0,i.max,2 ,1]];
  ResourcesPermanent = [[res.water,1800]];
  ResourcesBuild = [[res.money,-300]];
  ResourcesMonthly = [[res.money,-10]];
  downgradeTyp = [52];
 }
 <52> {
  name = "polluted water pump";structPath = "../Data/texture/urban/water/WPp";
  AreaDependent = [[area.water,1,i.max,5, 1],[area.pollution,0,i.max,1 ,0]];
  ResourcesMonthly = [[res.money,-10]];
  upgradeTyp = [51];
 }
 <53> {
  name = "water tower";structPath = "../Data/texture/urban/water/WT";
  AreaDependent = [[area.pollution,0,i.max,2 ,1]];
  downgradeTyp = [54];
  ResourcesPermanent = [[res.water,1400]];
  ResourcesBuild = [[res.money,-600]];
  ResourcesMonthly = [[res.money,-10]];
 }
 <54> {
  name = "polluted water tower";structPath = "../Data/texture/urban/water/WTp";
  AreaDependent = [[area.pollution,0,i.max,1 ,0]];
  ResourcesMonthly = [[res.money,-10]];
  upgradeTyp = [53];
 }
 <55> {
  name = "sewage plant"; size = 3; structPath = "../Data/texture/urban/water/KW";
  AreaDependent = [[area.road,100,i.max,5, 1]];
  ResourcesBuild = [[res.money,-15000]];
  ResourcesMonthly = [[res.money,-10],[res.waste,100]];
 }

 <58> {
  name = "landfill"; groundPath = "../Data/texture/urban/disposal/MD";
  ResourcesDependent = [[res.waste,200,i.max,effect.up,0]];
  ResourcesEffect = [[effect.up,res.waste,-200]];
  ResourcesMonthly = [[res.money,-10]];
  upgradeTyp = [59];
  ResourcesBuild = [[res.money,-50]];
 }
 <59> {
  name = "filed landfill";
  groundMode = 1;
    groundNeighbors = [59]; 
  groundPath = "../Data/texture/urban/disposal/MDF";
  structPath = "../Data/texture/urban/disposal/rubbish";

  AreaPermanent = [[area.pollution,5,-3]];
  ResourcesDependent = [[res.waste,i.min,0,effect.down,0]];
  ResourcesEffect = [[effect.down,res.waste,+200]];
  ResourcesMonthly = [[res.money,-10],[res.waste,-20]];
  downgradeTyp = [58];
 }
 <60> {
  name = "incinerator";size = 2;structPath = "../Data/texture/urban/disposal/MV";
  AreaDependent = [[area.road,100,i.max,5, 1]];
  AreaPermanent = [[area.pollution,12,-30]];
  ResourcesMonthly = [[res.money,-10],[res.waste,-1000]];
  ResourcesBuild = [[res.money,-700]];
 }


 // zones 61 to 90

 <61> {
  name = "Residential"; structPath = "../Data/texture/urban/Residential/W0";
  AreaDependent = [[area.road,1,i.max,effect.deacy, 1]];
  decayTyp = [0];
  buildMode = bmode.rnarea;
 }
 <62> {
  name = "Residential"; structPath = "../Data/texture/urban/Residential/W1";
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  downgradeTyp = [61];
  buildMode = bmode.rnarea;
 }
 <63> {
  name = "Residential"; structPath = "../Data/texture/urban/Residential/W2";
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  downgradeTyp = [62];
  buildMode = bmode.rnarea;
 }

 <71>  {
  name = "Comercial"; structPath = "../Data/texture/urban/Comercial/G0";
  AreaDependent = [[area.road,100,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 <72> {
  name = "Comercial"; structPath = "../Data/texture/urban/Comercial/G1";
  AreaDependent = [[area.road,100,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 <73>:72{}

 <75> {
  name = "Comercial"; structPath = "../Data/texture/urban/Comercial/G4";
  structMode = gmode.st; structNeighbors = [21,22,23,24];
  AreaDependent = [[area.road,100,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }

 <81> {
  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/B0";
  buildMode = bmode.rnarea;
 }
 <82>:81{}
 <83> {
  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/I0";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 <84> {

  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/I1";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }

 <87> {
  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/I2";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }


 // public 91 to 110


 <91> {
  name = "small fire department"; size = 2; structPath = "../Data/texture/urban/public/FW1";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 <92> {
  name = "large fire department"; size = 3; structPath = "../Data/texture/urban/public/FW2";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 <93> {
  name = "small police department"; size = 2; structPath = "../Data/texture/urban/public/PW1";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 <94> {
  name = "large police departmen"; size = 3; structPath = "../Data/texture/urban/public/PW2";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 <95> {
  name = "Comercial"; size = 3; structPath = "../Data/texture/urban/public/G";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }

 <102> {
  name = "hospital"; size = 3; structPath = "../Data/texture/urban/public/KH";
 }
