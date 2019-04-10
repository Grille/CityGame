//
<button_0>{name = "terrain" }
<button_1>{name = "traffic" }
<button_2>{name = "supply" }
<button_3>{name = "zones" }
<button_4>{name = "public" }

<zone>{typ = typ.zone;mode=mode.area}

//------------- < terrain > -------------
<tool>{color = [170,170,170]}
<tree>{mode=mode.brush;color = [115,207,92]}

<terrain_0>{
  name = "tree"
  typ = typ.label
}
<terrain_1>:tree{
  name = "conifer" 
  value = obj.conifer
}
<terrain_2>:tree{
  name = "deciduous" 
  value = obj.deciduous
}
<terrain_3>:tree{
  name = "palm" 
  value = obj.palm
}
<terrain_4>{
  name = "tools"
  typ = typ.label
}
<terrain_5>:tool{
  name = "demolish" 
  value = obj.empety
}
<terrain_6>:tool{
  typ = typ.zone
  name = "dezone" 
  value = 0
}

//------------- < traffic > -------------
<road>{color = [217, 142, 242];mode=mode.line}

<traffic_0>{
  name = "Road"
  typ = typ.label
}
<traffic_1>:road{
  name = "dirt way" 
  value = obj.dirtWay
  replace = [obj.water,obj.woodBridge]
}
<traffic_2>:road{
  name = "medium road" 
  value = obj.mediumRoad
  replace = [obj.water,obj.mediumBridge]
}
<traffic_3>:road{
  name = "large road" 
  value = obj.largeRoad
  replace = [obj.water,obj.largeBridge, obj.saltwater,obj.oceanBridge]
}

//------------- < supply > -------------
<energy>{color = [237, 232, 137]}
<water>{color = [125, 143, 232]}
<disposal>{color = [198, 166, 113]}

<supply_0>{
  name = "Energy"
  typ = typ.label
}
<supply_1>:energy{
  name = "coal power plant" 
  value = obj.coalPowerPlant
}
<supply_2>:energy{
  name = "gas power plant" 
  value = obj.gasPowerPlant
}
<supply_3>:energy{
  name = "nuclear power plant" 
  value = obj.nuclearPowerPlant
}
<supply_4>:energy{
  name = "solar power plant" 
  value = obj.solarPowerPlant
}
<supply_5>:energy{
  name = "wind power plant" 
  value = obj.windPowerPlant
}

<supply_6>{
  name = "water"
  typ = typ.label
}
<supply_7>:water{
  name = "water pump" 
  value = obj.waterPump
}
<supply_8>:water{
  name = "water tower" 
  value = obj.waterTower
}
<supply_9>:water{
  name = "sewage plant" 
  value = obj.sewagePlant
}

<supply_10>{
  name = "disposal"
  typ = typ.label
}
<supply_11>:disposal{
  name = "landfill" 
  mode=mode.area
  value = obj.landfill
}
<supply_12>:disposal{
  name = "incinerator" 
  value = obj.incinerator
}

//------------- < zones > -------------
<residential>:zone{color = [148, 237, 137]}
<comercial>:zone{color = [137, 198, 237]}
<industrial>:zone{color = [237, 195, 137]}

<zones_0>{
  name = "residential"
  typ = typ.label
}
<zones_1>:residential{
  name = "Light residential" 
  value = 0
}
<zones_2>:residential{
  name = "Medium residential" 
  value = 1
}
<zones_3>:residential{
  name = "Dense residential" 
  value = 2
}

<zones_4>{
  name = "comercial"
  typ = typ.label
}
<zones_5>:comercial{
  name = "Light comercial" 
  value = 3
}
<zones_6>:comercial{
  name = "Medium comercial" 
  value = 4
}
<zones_7>:comercial{
  name = "Dense comercial" 
  value = 5
}

<zones_8>{
  name = "industrial"
  typ = typ.label
}
<zones_9>:industrial{
  name = "Light industrial" 
  value = 6
}
<zones_10>:industrial{
  name = "Medium industrial" 
  value = 7
}
<zones_11>:industrial{
  name = "Dense industrial" 
  value = 8
}

<zones_12>{
  name = "tools"
  typ = typ.label
}
<zones_13>:tool{
  typ = typ.zone
  name = "dezone" 
  value = 0
}

//------------- < public > -------------
<public>{color = [237, 195, 137]}

<public_0>{
  name = "safety"
  typ = typ.label
}
<public_1>:public{
  name = "small fire department" 
  value = obj.smallFireDepartment
}
<public_2>:public{
  name = "large fire department" 
  value = obj.largeFireDepartment
}
<public_3>:public{
  name = "small police department" 
  value = obj.smallPoliceDepartment
}
<public_4>:public{
  name = "large police department" 
  value = obj.largePoliceDepartment
}
<public_5>:public{
  name = "hospital" 
  value = obj.hospital
}

