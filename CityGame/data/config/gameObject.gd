
Enums{ 
  effect{ not, up, down ,break ,deacy ,destroy ,entf}
  res{ money, energy, water, waste}
  area{ water, pollution, road, saltwater}
  i{ min=-32768, max=32767}
  gmode{ not=0, all=1, focu=2, foen=3, cuen=4, fo=5, cu=6, en=7, st=8}
  bmode{ single, brush, rnline, eqline, cnline, rnarea, eqarea}
}
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
  canBuiltOn = [0,3,4,5,6,7,8,9,13];

}

 ID-0 {
  canBuiltOn = [];
 }
 


 // 1 to 20 nature
 ID-1 {
  name = "Water"; 
  groundMode = effect.up;
  groundNeighbors = [1,2,25]; 
  groundPath = "../Data/texture/nature/water_0";

  AreaPermanent = [area.water,1,1];
 }
 ID-2:1 {
  name = "Saltwater"; 
  groundPath = "../Data/texture/nature/saltwater_0";

  AreaPermanent = [area.saltwater,1,1]
 }

 //	<---- Forest ---->
 ID-3 {
  
  name = "Conifer";  
  structPath = "../Data/texture/nature/Conifer";
  buildMode = 1;
  canBuiltOn = [0];
  

  downgradeTyp = [6];
  destroyTyp = [9];
  
  AreaPermanent = [[area.pollution,3,2]];
  
  AreaDependent = [[area.pollution,0, i.max ,effect.down ,1]];
 
 }
 ID-4:3 {
  
  name = "Deciduous"; 
  structPath = "../Data/texture/nature/Deciduous";
  downgradeTyp = [7];
  
 }
 ID-5:3 {
  
  name = "Palm"; 
  structPath = "../Data/texture/nature/Palm";
  downgradeTyp = [8];
  
 }
 ID-6 {
  
  name = "Conifer dead"; 
  structPath = "../Data/texture/nature/ConiferDead";
  
  upgradeTyp = [3];
  destroyTyp = [9];
 
  AreaDependent = [[area.pollution,0,i.max ,effect.up ,0]];
 
 }
 
 ID-7:6 {
  
  name = "Oak dead"; 
  structPath = "../Data/texture/nature/DeciduousDead";
  
  upgradeTyp = [4];
 }
 
 ID-8:6 {
  
  name = "Palm dead"; 
  structPath = "../Data/texture/nature/PalmDead";
  
  upgradeTyp = [5];
 }



 ID-9 {
  name = "Destroyed forest"; 
  structPath = "../Data/texture/nature/DestroyedForest";
 }

 ID-10 {
  name = "Stone Small"; structPath = "../Data/texture/nature/StoneSmall";
 }
 ID-11 {
  name = "Stone large";
 }
 ID-12 {
  name = "Garbage";
 }
 ID-13 {
  name = "debris"; structPath = "../Data/texture/urban/debris/debris";canBuiltOn = [];
 }

//	<---- Road ---->

 ID-21 {
  name = "dirt way"; 
  buildMode = 4;
  repalceTyp = [1,25];

  groundMode = gmode.all; 
  groundNeighbors = [21,22,23,24,25]; 
  groundPath = "../Data/texture/urban/road/FW1_2";
  AreaPermanent = [area.road,1,100]; //typ,size,value
  ResourcesBuild = [res.money,-8];
  ResourcesMonthly = [res.money,-2];
 }

 ID-23:21 {
  name = "medium road"; 
  canBuiltOn + [21,22];
  groundPath = "../Data/texture/urban/road/RM_1";
  AreaPermanent + [area.road,2,1];
  ResourcesBuild = [res.money,-16];
  ResourcesMonthly = [res.money,-4];
 }
 ID-24:21 {
  name = "large road"; 
  canBuiltOn + [21,22,23];
  groundPath = "../Data/texture/urban/road/RL_1";
  AreaPermanent + [area.road,4,1];
  //ResourcesBuild = [res.money,-24];
  ResourcesMonthly = [res.money,-8];
 }
 ID-25:1 {
  demolitionTyp = [1];
  decayTyp = [1];
  destroyTyp = [1];
  name = "wood bridge"; 
  canBuiltOn = [1];
  structMode = gmode.st;
  structNeighbors = [21,22,23,24,25]; 
  structPath = "../Data/texture/urban/road/WB";
 }

 // supply 41 to 60

 ID-41 {
  name = "coal power plant";size = 3; 
  structPath = "../Data/texture/urban/power/KKW";

  AreaPermanent = [[1,8,-15],[1,12,-15],[1,16,-15]];
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,5000]];
  ResourcesBuild = [[res.money,-5000]];
 }
 ID-42 {
  name = "gas power plant"; size = 3; 
  structPath = "../Data/texture/urban/power/GKW";

  AreaPermanent = [[1,10,-15]];
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,3000]];
  ResourcesBuild = [[res.money,-4500]];
  ResourcesMonthly = [[res.money,-10]];
 }
 ID-43 {
  name = "nuclear power plant"; size = 3; 
  structPath = "../Data/texture/urban/power/AKW";

  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,15000]];
  ResourcesBuild = [[res.money,-20000]];
  ResourcesMonthly = [[res.money,-10]];
 }
 ID-44 {
  name = "solar power plant"; size = 3; 
  structPath = "../Data/texture/urban/power/SKW";

  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,2000]];
  ResourcesBuild = [[res.money,-15000]];
  ResourcesMonthly = [[res.money,-10]];
 }
 ID-45 {
  name = "wind power plant"; size = 1; 
  structPath = "../Data/texture/urban/power/WKW";

  ResourcesPermanent = [[res.energy,600]];
  ResourcesBuild = [[res.money,-2500]];
  ResourcesMonthly = [[res.money,-10]];
 }

 ID-51 {
  name = "water pump";structPath = "../Data/texture/urban/water/WP";
  AreaDependent = [[0,1,i.max,5, 1],[1,0,i.max,2 ,1]];
  ResourcesPermanent = [[res.water,1800]];
  ResourcesBuild = [[res.money,-300]];
  ResourcesMonthly = [[res.money,-10]];
  downgradeTyp = [52];
 }
 ID-52 {
  name = "polluted water pump";structPath = "../Data/texture/urban/water/WPp";
  AreaDependent = [[0,1,i.max,5, 1],[1,0,i.max,1 ,0]];
  ResourcesMonthly = [[res.money,-10]];
  upgradeTyp = [51];
 }
 ID-53 {
  name = "water tower";structPath = "../Data/texture/urban/water/WT";
  AreaDependent = [[1,0,i.max,2 ,1]];
  downgradeTyp = [54];
  ResourcesPermanent = [[res.water,1400]];
  ResourcesBuild = [[res.money,-600]];
  ResourcesMonthly = [[res.money,-10]];
 }
 ID-54 {
  name = "polluted water tower";structPath = "../Data/texture/urban/water/WTp";
  AreaDependent = [[1,0,i.max,1 ,0]];
  ResourcesMonthly = [[res.money,-10]];
  upgradeTyp = [53];
 }
 ID-55 {
  name = "sewage plant"; size = 3; structPath = "../Data/texture/urban/water/KW";
  AreaDependent = [[2,100,i.max,5, 1]];
  ResourcesBuild = [[res.money,-15000]];
  ResourcesMonthly = [[res.money,-10]];
 }

 ID-58 {
  name = "landfill"; groundPath = "../Data/texture/urban/disposal/MD";
  ResourcesDependent = [[res.waste,500,i.max,effect.up,0]];
  ResourcesEffect = [[effect.up,res.waste,-100]];
  ResourcesMonthly = [[res.money,-10]];
  upgradeTyp = [59];
  ResourcesBuild = [[res.money,-50]];
 }
 ID-59 {
  name = "filed landfill";
  groundMode = 1;
    groundNeighbors = [59]; 
  groundPath = "../Data/texture/urban/disposal/MDF";
  structPath = "../Data/texture/urban/disposal/rubbish";

  AreaPermanent = [[1,5,-3]];
  ResourcesDependent = [[res.waste,i.min,100,effect.down,0]];
  ResourcesEffect = [[effect.down,res.waste,+100]];
  ResourcesMonthly = [[res.money,-10]];
  downgradeTyp = [58];
 }
 ID-60 {
  name = "incinerator";size = 2;structPath = "../Data/texture/urban/disposal/MV";
  AreaDependent = [[2,100,i.max,5, 1]];
  AreaPermanent = [[1,8,-15],[1,12,-15]];
  ResourcesMonthly = [[res.money,-10],[res.waste,-1000]];
  ResourcesBuild = [[res.money,-7000]];
 }


 // zones 61 to 90

 ID-61 {
  name = "Residential"; structPath = "../Data/texture/urban/Residential/W0";
  AreaDependent = [[area.road,1,i.max,effect.deacy, 1]];
  int[] decayTyp = [0];
  buildMode = bmode.rnarea;
 }
 ID-62 {
  name = "Residential"; structPath = "../Data/texture/urban/Residential/W1";
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  int[] downgradeTyp = [61];
  buildMode = bmode.rnarea;
 }
 ID-63 {
  name = "Residential"; structPath = "../Data/texture/urban/Residential/W2";
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  int[] downgradeTyp = [62];
  buildMode = bmode.rnarea;
 }

 ID-71  {
  name = "Comercial"; structPath = "../Data/texture/urban/Comercial/G0";
  AreaDependent = [[area.road,100,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 ID-72 {
  name = "Comercial"; structPath = "../Data/texture/urban/Comercial/G1";
  AreaDependent = [[area.road,100,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }

 ID-75 {
  name = "Comercial"; structPath = "../Data/texture/urban/Comercial/G4";
  structMode = gmode.st; structNeighbors = [21,22,23,24];
  AreaDependent = [[area.road,100,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }

 ID-81 {
  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/B0";
 }

 ID-83 {
  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/I0";
  AreaDependent = [[2,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 ID-84 {

  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/I1";
  AreaDependent = [[2,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }

 ID-87 {
  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/I2";
  AreaDependent = [[2,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }


 // public 91 to 110


 ID-91 {
  name = "small fire department"; size = 2; structPath = "../Data/texture/urban/public/FW1";
  AreaDependent = [[2,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 ID-92 {
  name = "large fire department"; size = 3; structPath = "../Data/texture/urban/public/FW2";
  AreaDependent = [[2,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 ID-93 {
  name = "small police department"; size = 2; structPath = "../Data/texture/urban/public/PW1";
  AreaDependent = [[2,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 ID-94 {
  name = "large police departmen"; size = 3; structPath = "../Data/texture/urban/public/PW2";
  AreaDependent = [[2,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }
 ID-95 {
  name = "Comercial"; size = 3; structPath = "../Data/texture/urban/public/G";
  AreaDependent = [[2,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
 }

 ID-102 {
  name = "hospital"; size = 3; structPath = "../Data/texture/urban/public/KH";
 }
