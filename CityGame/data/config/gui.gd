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
  value = 3
}
<terrain_2>:tree{
  name = "deciduous" 
  value = 4
}
<terrain_3>:tree{
  name = "palm" 
  value = 5
}
<terrain_4>{
  name = "tools"
  typ = typ.label
}
<terrain_5>:tool{
  name = "demolish" 
  value = 0
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
  value = 21
  replace = [[1,25]]
}
<traffic_2>:road{
  name = "medium road" 
  value = 23
  replace = [[1,26]]
}
<traffic_3>:road{
  name = "large road" 
  value = 24
  replace = [[1,27],[2,28]]
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
  value = 41
}
<supply_2>:energy{
  name = "gas power plant" 
  value = 42
}
<supply_3>:energy{
  name = "nuclear power plant" 
  value = 43
}
<supply_4>:energy{
  name = "solar power plant" 
  value = 44
}
<supply_5>:energy{
  name = "wind power plant" 
  value = 45
}

<supply_6>{
  name = "water"
  typ = typ.label
}
<supply_7>:water{
  name = "water pump" 
  value = 51
}
<supply_8>:water{
  name = "water tower" 
  value = 53
}
<supply_9>:water{
  name = "sewage plant" 
  value = 55
}

<supply_10>{
  name = "disposal"
  typ = typ.label
}
<supply_11>:disposal{
  name = "landfill" 
  mode=mode.area
  value = 58
}
<supply_12>:disposal{
  name = "incinerator" 
  value = 60
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
  value = 1
}
<zones_2>:residential{
  name = "Medium residential" 
  value = 2
}
<zones_3>:residential{
  name = "Dense residential" 
  value = 3
}

<zones_4>{
  name = "comercial"
  typ = typ.label
}
<zones_5>:comercial{
  name = "Light comercial" 
  value = 4
}
<zones_6>:comercial{
  name = "Medium comercial" 
  value = 5
}
<zones_7>:comercial{
  name = "Dense comercial" 
  value = 6
}

<zones_8>{
  name = "industrial"
  typ = typ.label
}
<zones_9>:industrial{
  name = "Light industrial" 
  value = 7
}
<zones_10>:industrial{
  name = "Medium industrial" 
  value = 8
}
<zones_11>:industrial{
  name = "Dense industrial" 
  value = 9
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
  value = 91
}
<public_2>:public{
  name = "large fire department" 
  value = 92
}
<public_3>:public{
  name = "small police department" 
  value = 93
}
<public_4>:public{
  name = "large police department" 
  value = 94
}
<public_5>:public{
  name = "hospital" 
  value = 102
}

