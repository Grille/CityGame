
 Attributes {
  string name;
  int initValue;
  bool physical;
  bool storable;

  name = "";
  initValue = 0;
  physical = false;
  storable = false;
 }

 <money> {
  initValue = 200000000;
  name = "money";
  //storable = true;
  //CanBeNegative = true;
 }

 <energy> {
  initValue = 200000;
  name = "energy";
 }

 <water> {
  initValue = 200000;
  name = "water";
 }

 <waste> {
  name = "waste";
  initValue = 0;
 }
