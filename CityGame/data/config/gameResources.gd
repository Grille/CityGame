
 Attributes {
  string name;
  int initValue;
  bool physical;
  bool storable;
 }

 Init {
  name = "";
  initValue = 0;
  physical = false;
  storable = false;
 }

 <0> {
  initValue = 200000000;
  name = "money";
  //storable = true;
  //CanBeNegative = true;
 }

 <1> {
  initValue = 200000;
  name = "energy";
 }

 <2> {
  initValue = 200000;
  name = "water";
 }

 <3> {
  name = "waste";
  initValue = 0;
 }
