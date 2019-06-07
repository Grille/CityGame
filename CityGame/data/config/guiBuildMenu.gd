//
<button_0>{name = "terrain" }
<button_1>{name = "traffic" }
<button_2>{name = "supply" }
<button_3>{name = "zones" }
<button_4>{name = "service" }
<button_5>{name = "public" }
<button_6>{name = "god" }

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
  mode=mode.area
  value = obj.empty
}
<terrain_6>:tool{
  typ = typ.zone
  name = "dezone" 
  mode=mode.area
  value = zone.empty
}

//------------- < traffic > -------------
<way>{color = [217, 142, 242];mode=mode.line}

<traffic_0>{
  name = "Road"
  typ = typ.label
}
/*
<traffic_1>:way{
  name = "dirt way" 
  value = obj.dirtWay
  replace = [obj.water,obj.woodBridge]
}
*/
<traffic_1>:way{
  name = "road" 
  value = obj.mediumRoad
  replace = [obj.water,obj.mediumBridge]
}
<traffic_2>:way{
  name = "large road" 
  value = obj.largeRoad
  replace = [obj.water,obj.largeBridge, obj.saltwater,obj.oceanBridge]
}
<traffic_3>{
  name = "Railroad"
  typ = typ.label
}
<traffic_4>:way{
  name = "rails"
    value = obj.railRoad
}
<traffic_5>{
  color = [217, 142, 242];
  name = "station"
}
<traffic_6>{
  name = "--"
  typ = typ.label
}
<traffic_7>{
  color = [217, 142, 242];
  name = "harbor"
}
<traffic_8>{
  color = [217, 142, 242];
  name = "airport"
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
  name = "powerline" 
  mode=mode.line
}
<supply_2>:energy{
  name = "coal power plant" 
  value = obj.coalPowerPlant
}
<supply_3>:energy{
  name = "gas power plant" 
  value = obj.gasPowerPlant
}
<supply_4>:energy{
  name = "nuclear power plant" 
  value = obj.nuclearPowerPlant
}
<supply_5>:energy{
  name = "solar power plant" 
  value = obj.solarPowerPlant
}
<supply_6>:energy{
  name = "wind power plant" 
  value = obj.windPowerPlant
}

<supply_7>{
  name = "water"
  typ = typ.label
}
<supply_8>:water{
  name = "water pump" 
  value = obj.waterPump
}
<supply_9>:water{
  name = "water tower" 
  value = obj.waterTower
}
<supply_10>:water{
  name = "sewage plant" 
  value = obj.sewagePlant
}

<supply_11>{
  name = "disposal"
  typ = typ.label
}
<supply_12>:disposal{
  name = "landfill" 
  mode=mode.area
  value = obj.landfill
}
<supply_13>:disposal{
  name = "recycling center" 
}
<supply_14>:disposal{
  name = "incinerator" 
  value = obj.incinerator
}
<supply_15>:disposal{
  name = "garbage power plant" 
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
  value = zone.r1
}
<zones_2>:residential{
  name = "Medium residential" 
  value = zone.r2
}
<zones_3>:residential{
  name = "Dense residential" 
  value = zone.r3
}

<zones_4>{
  name = "comercial"
  typ = typ.label
}
<zones_5>:comercial{
  name = "Light comercial" 
  value = zone.c1
}
<zones_6>:comercial{
  name = "Medium comercial" 
  value = zone.c2
}
<zones_7>:comercial{
  name = "Dense comercial" 
  value = zone.c3
}

<zones_8>{
  name = "industrial"
  typ = typ.label
}
<zones_9>:industrial{
  name = "Light industrial" 
  value = zone.i1
}
<zones_10>:industrial{
  name = "Medium industrial" 
  value = zone.i2
}
<zones_11>:industrial{
  name = "Dense industrial" 
  value = zone.i3
}

<zones_12>{
  name = "tools"
  typ = typ.label
}
<zones_13>:tool{
  typ = typ.zone
  name = "dezone" 
  value = zone.empty
}

//------------- < service > -------------
<fire>{color = [200, 128, 128]}
<police>{color = [125, 143, 232]}
<service>{color = [222, 222, 180]}

<service_0>{
  name = "Fire"
  typ = typ.label
}
<service_1>:fire{
  name = "small fire department" 
  value = obj.smallFireDepartment
}
<service_2>:fire{
  name = "large fire department" 
  value = obj.largeFireDepartment
}
<service_3>{
  name = "Police"
  typ = typ.label
}
<service_4>:police{
  name = "small police department" 
  value = obj.smallPoliceDepartment
}
<service_5>:police{
  name = "large police department" 
  value = obj.largePoliceDepartment
}
<service_6>:police{
  name = "prison" 
  value = obj.prison
}
<service_7>{
  name = "education & health"
  typ = typ.label
}
<service_8>:service{
  name = "school" 
}
<service_9>:service{
  name = "hospital" 
  value = obj.hospital
}

//------------- < public > -------------
<public>{color = [237, 195, 137]}
<public_0>{
  name = "administration"
  typ = typ.label
}
<public_1>:public{
  name = "city hall"
}
<public_2>{
  name = "public"
  typ = typ.label
}
<public_3>:public{
  name = "small park"
}
<public_4>:public{
  name = "large park"
}

//------------- < god > -------------
<god_0>{
  name = "disasters"
  typ = typ.label
}
<god_1>:fire{
  name = "fire"
}
<god_2>:fire{
  name = "tornado"
}
