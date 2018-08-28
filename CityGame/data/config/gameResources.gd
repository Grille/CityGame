
 Attributes {
  string name;
  int value;
  bool physical;
  bool storable;
 }

 Init {
  name = "";
  value = 0;
  physical = false;
  storable = false;
 }

 ID-0 {
  value = 200000000;
  name = "money";
  //storable = true;
  //CanBeNegative = true;
 }

 ID-1 {
 value = 200000;
  name = "energy";
 }

 ID-2 {
 value = 200000;
  name = "water";
 }

 ID-3 {
  name = "waste";
  value = 0;
 }
