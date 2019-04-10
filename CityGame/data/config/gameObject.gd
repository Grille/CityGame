
Attributes {
  //Basic;
  string name = "";
  byte buildMode; //0 single,1 brush,2 rnd_line,3 con_line,4 equal_line,5 rnd_area, 6 equal_area
  ref[] repalceTyp; //[[tileTyp,replaceTyp]]

  //Graphic
  string structPath, groundPath;
  byte groundMode, structMode; //0:(else),1:(all),2:(straight,fork,ends,else),3:(straight,curves,fork,else),4:(straight,curves,ends,else), 5:(straight,fork,else),6:(straight,curves,else),7:(straight,ends,else),8:(straight,else)
  
  //mode{not,all,focu,foen,cuen,fo,cu,en,st}
  
  ref[] structNeighbors, groundNeighbors;
  byte size;

  script onupdate;

  //Simulation 1;
  ref[] upgradeTyp,downgradeTyp,demolitionTyp,decayTyp,destroyTyp;
  ref[] canBuiltOn;

  //Simulation 2;
  int[] AreaPermanent;      //[[typ,size,value]]
  int[] AreaDependent;      //[[typ,minValue,maxValue,effects,mode]]
  int[] ResourcesEffect;    //[[firedEffect,typ,value]]
  int[] ResourcesBuild;     //[[typ,value]]
  int[] ResourcesPermanent; //[[typ,value]]
  int[] ResourcesMonthly;   //[[typ,value]]
  int[] ResourcesDependent; //[[typ,minValue,maxValue,effects,mode]]

  repalceTyp = [];
  groundNeighbors = [];
  structNeighbors = [];

  demolitionTyp = [empety];
  size = 1;
  decayTyp = [debris];
  destroyTyp = [debris];
  canBuiltOn = [empety,conifer to forestDestroyed,debris];
}

<empety> {
  canBuiltOn = [];

  ResourcesDependent = [[res.waste,1000,i.max,effect.down,0]];
  ResourcesEffect = [[effect.down,res.waste,-100]];
  downgradeTyp = [garbage];
}
 


// 1 to 20 nature
<water> {
  name = "Water"; 
  groundMode = effect.up;
  groundNeighbors = [water,saltwater,woodBridge to oceanBridge]; 
  groundPath = "../Data/texture/nature/water_0";

  AreaPermanent = [area.water,1,1];
}
<saltwater>:water {
  name = "Saltwater"; 
  groundPath = "../Data/texture/nature/saltwater_0";

  AreaPermanent = [area.saltwater,1,1]
}

//	<---- Forest ---->
<conifer> {
  
  name = "Conifer";  
  structPath = "../Data/texture/nature/Conifer";
  buildMode = 1;
  canBuiltOn = [empety];
  

  downgradeTyp = [coniferDead];
  destroyTyp = [forestDestroyed];
  
  AreaPermanent = [[area.pollution,6,1]];
  
  AreaDependent = [[area.pollution,0, i.max ,effect.down ,1]];
 
}
<deciduous>:conifer {
  name = "Deciduous"; 
  structPath = "../Data/texture/nature/Deciduous";
  downgradeTyp = [deciduousDead];
}
<palm>:conifer {
  name = "Palm"; 
  structPath = "../Data/texture/nature/Palm";
  downgradeTyp = [palmDead];
}

<coniferDead> {
  name = "Conifer dead"; 
  structPath = "../Data/texture/nature/ConiferDead";
  upgradeTyp = [conifer];
  destroyTyp = [forestDestroyed];
  AreaPermanent = [area.pollution,5,-1];
  AreaDependent = [area.pollution,0,i.max ,effect.up ,0];
  onupdate = (){
    
    System.Console.WriteLine("update");
	//if (area.pollution < 0) 
	//me.changeTo(me.upgradeTyp);
  }
}
<deciduousDead>:coniferDead {
  name = "Oak dead"; 
  structPath = "../Data/texture/nature/DeciduousDead";
  upgradeTyp = [deciduous];
}
<palmDead>:coniferDead {
  name = "Palm dead"; 
  structPath = "../Data/texture/nature/PalmDead";
  upgradeTyp = [palm];
}

<forestDestroyed> {
  name = "Destroyed forest"; 
  structPath = "../Data/texture/nature/DestroyedForest";
}


<stoneSmall> {
  name = "Stone Small"; structPath = "../Data/texture/nature/StoneSmall";
}
<stoneLarge> {
  name = "Stone large";
}

<garbage> {
  name = "Garbage";structPath = "../Data/texture/urban/disposal/rubbish";
  canBuiltOn = [empety];
  AreaPermanent = [[area.pollution,10,-4]];
  ResourcesDependent = [[res.waste,i.min,800,effect.up,0]];
  ResourcesEffect = [[effect.up,res.waste,+100]];
  ResourcesMonthly = [res.waste,-10];
  upgradeTyp = [empety];
}
<debris> {
  name = "debris"; structPath = "../Data/texture/urban/debris/debris";canBuiltOn = [];
}

//	<---- Road ---->

<dirtWay> {
  name = "dirt way"; 
  buildMode = 4;

  groundMode = gmode.all; 
  groundNeighbors = [dirtWay to oceanBridge]; 
  groundPath = "../Data/texture/urban/road/FW1_2";
  AreaPermanent = [area.road,1,100]; //typ,size,value
  ResourcesBuild = [res.money,-8];
  ResourcesMonthly = [res.money,-2];
}

<mediumRoad>:dirtWay {
  name = "medium road"; 

  canBuiltOn + [dirtWay];
  groundPath = "../Data/texture/urban/road/RM_1";
  AreaPermanent + [area.road,2,1];
  ResourcesBuild = [res.money,-16];
  ResourcesMonthly = [res.money,-4];
}
<largeRoad>:mediumRoad {
  name = "large road"; 
  canBuiltOn + [mediumRoad];
  groundPath = "../Data/texture/urban/road/RL_1";
  AreaPermanent + [area.road,4,1];
  //ResourcesBuild = [res.money,-24];
  ResourcesMonthly = [res.money,-8];
}
<woodBridge>:water {
  demolitionTyp = [water];
  decayTyp = [water];
  destroyTyp = [water];
  name = "wood bridge"; 
  canBuiltOn = [water];
  structMode = gmode.st;
  structNeighbors = [dirtWay to oceanBridge];
  groundNeighbors = [water,saltwater,woodBridge]; 
  structPath = "../Data/texture/urban/road/BW";
}
 <mediumBridge>:woodBridge {
  name = "medium bridge"; 
    groundNeighbors = [water,saltwater,mediumBridge]; 
  structPath = "../Data/texture/urban/road/BM";
}
 <largeBridge>:woodBridge {
  name = "large bridge"; 
    groundNeighbors = [water,saltwater,largeBridge]; 
  structPath = "../Data/texture/urban/road/BL";
}
<oceanBridge>:saltwater {
  demolitionTyp = [saltwater];
  decayTyp = [saltwater];
  destroyTyp = [saltwater];
  name = "large bridge"; 
  canBuiltOn = [saltwater];
  structMode = gmode.st;
  structNeighbors = [dirtWay to oceanBridge];
  groundNeighbors = [water,saltwater,oceanBridge]; 
  structPath = "../Data/texture/urban/road/BL";
}

 // supply 41 to 60

<coalPowerPlant> {
  name = "coal power plant";size = 3; 
  structPath = "../Data/texture/urban/power/KKW";

  AreaPermanent = [[area.pollution,16,-45]];
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,5000]];
  ResourcesBuild = [[res.money,-5000]];
  ResourcesMonthly = [res.waste,100];
}
<gasPowerPlant> {
  name = "gas power plant"; size = 3; 
  structPath = "../Data/texture/urban/power/GKW";

  AreaPermanent = [[area.pollution,10,-15]];
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,3000]];
  ResourcesBuild = [[res.money,-4500]];
  ResourcesMonthly = [[res.money,-10],[res.waste,50]];
}
<nuclearPowerPlant> {
  name = "nuclear power plant"; size = 3; 
  structPath = "../Data/texture/urban/power/AKW";

  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,15000]];
  ResourcesBuild = [[res.money,-20000]];
  ResourcesMonthly = [[res.money,-10],[res.waste,1000]];
}
<solarPowerPlant> {
  name = "solar power plant"; size = 3; 
  structPath = "../Data/texture/urban/power/SKW";

  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  ResourcesPermanent = [[res.energy,2000]];
  ResourcesBuild = [[res.money,-15000]];
  ResourcesMonthly = [[res.money,-10]];
}
<windPowerPlant> {
  name = "wind power plant"; size = 1; 
  structPath = "../Data/texture/urban/power/WKW";

  ResourcesPermanent = [[res.energy,600]];
  ResourcesBuild = [[res.money,-2500]];
  ResourcesMonthly = [[res.money,-10]];
 }

<waterPump> {
  name = "water pump";structPath = "../Data/texture/urban/water/WP";
  AreaDependent = [[area.water,1,i.max,5, 1],[area.pollution,0,i.max,2 ,1]];
  ResourcesPermanent = [[res.water,1800]];
  ResourcesBuild = [[res.money,-300]];
  ResourcesMonthly = [[res.money,-10]];
  downgradeTyp = [pollutedWaterPump];
}
<pollutedWaterPump> {
  name = "polluted water pump";structPath = "../Data/texture/urban/water/WPp";
  AreaDependent = [[area.water,1,i.max,5, 1],[area.pollution,0,i.max,1 ,0]];
  ResourcesMonthly = [[res.money,-10]];
  upgradeTyp = [waterPump];
}
<waterTower> {
  name = "water tower";structPath = "../Data/texture/urban/water/WT";
  AreaDependent = [[area.pollution,0,i.max,2 ,1]];
  downgradeTyp = [pollutedWaterTower];
  ResourcesPermanent = [[res.water,1400]];
  ResourcesBuild = [[res.money,-600]];
  ResourcesMonthly = [[res.money,-10]];
}
<pollutedWaterTower> {
  name = "polluted water tower";structPath = "../Data/texture/urban/water/WTp";
  AreaDependent = [[area.pollution,0,i.max,1 ,0]];
  ResourcesMonthly = [[res.money,-10]];
  upgradeTyp = [waterTower];
}
<sewagePlant> {
  name = "sewage plant"; size = 3; structPath = "../Data/texture/urban/water/KW";
  AreaDependent = [[area.road,100,i.max,5, 1]];
  ResourcesBuild = [[res.money,-15000]];
  ResourcesMonthly = [[res.money,-10],[res.waste,100]];
}

<landfill> {
  name = "landfill"; groundPath = "../Data/texture/urban/disposal/MD";
  ResourcesDependent = [[res.waste,200,i.max,effect.up,0]];
  ResourcesEffect = [[effect.up,res.waste,-200]];
  ResourcesMonthly = [[res.money,-10]];
  upgradeTyp = [filedLandfill];
  ResourcesBuild = [[res.money,-50]];
}
<filedLandfill> {
  name = "filed landfill";
  groundMode = 1;
  groundNeighbors = [filedLandfill]; 
  groundPath = "../Data/texture/urban/disposal/MDF";
  structPath = "../Data/texture/urban/disposal/rubbish";

  AreaPermanent = [[area.pollution,5,-3]];
  ResourcesDependent = [[res.waste,i.min,0,effect.down,0]];
  ResourcesEffect = [[effect.down,res.waste,+200]];
  ResourcesMonthly = [[res.money,-10],[res.waste,-20]];
  downgradeTyp = [landfill];
}
<incinerator> {
  name = "incinerator";size = 2;structPath = "../Data/texture/urban/disposal/MV";
  AreaDependent = [[area.road,100,i.max,5, 1]];
  AreaPermanent = [[area.pollution,12,-30]];
  ResourcesMonthly = [[res.money,-10],[res.waste,-1000]];
  ResourcesBuild = [[res.money,-700]];
}


// zones 61 to 90

<res1> {
  name = "Residential"; structPath = "../Data/texture/urban/Residential/W0";
  AreaDependent = [[area.road,1,i.max,effect.deacy, 1]];
  decayTyp = [empety];
  buildMode = bmode.rnarea;
}
<res2> {
  name = "Residential"; structPath = "../Data/texture/urban/Residential/W1";
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  buildMode = bmode.rnarea;
}
<res3> {
  name = "Residential"; structPath = "../Data/texture/urban/Residential/W2";
  AreaDependent = [[area.road,100,i.max,effect.deacy, 1]];
  buildMode = bmode.rnarea;
}

<com1>  {
  name = "Comercial"; structPath = "../Data/texture/urban/Comercial/G0";
  AreaDependent = [[area.road,100,i.max,5, 1]];
  buildMode = bmode.rnarea;
}
<com2> {
  name = "Comercial"; structPath = "../Data/texture/urban/Comercial/G1";
  AreaDependent = [[area.road,100,i.max,5, 1]];
  buildMode = bmode.rnarea;
}

<com3> {
  name = "Comercial"; structPath = "../Data/texture/urban/Comercial/G4";
  structMode = gmode.st; structNeighbors = [dirtWay to largeRoad];
  AreaDependent = [[area.road,100,i.max,5, 1]];
  buildMode = bmode.rnarea;
}

<ind1> {
  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/B0";
  buildMode = bmode.rnarea;
}
<ind2> {
  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/I0";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
}
<ind3> {

  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/I1";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
}

<ind4> {
  name = "Industrieal"; structPath = "../Data/texture/urban/Industrieal/I2";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
}


// public 91 to 110


<smallFireDepartment> {
  name = "small fire department"; size = 2; structPath = "../Data/texture/urban/public/FW1";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
}
<largeFireDepartment> {
  name = "large fire department"; size = 3; structPath = "../Data/texture/urban/public/FW2";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
}
<smallPoliceDepartment> {
  name = "small police department"; size = 2; structPath = "../Data/texture/urban/public/PW1";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
}
<largePoliceDepartment> {
  name = "large police departmen"; size = 3; structPath = "../Data/texture/urban/public/PW2";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
}
<_> {
  name = "Comercial"; size = 3; structPath = "../Data/texture/urban/public/G";
  AreaDependent = [[area.road,1,i.max,5, 1]];
  buildMode = bmode.rnarea;
}

<hospital> {
  name = "hospital"; size = 3; structPath = "../Data/texture/urban/public/KH";
}
