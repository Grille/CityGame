{
 Template
 {
  string name = " ";
  int value = 0;
  bool CanBeNegative = false;
  bool storable = false;
 }

 ID=0; // money
 {
  value = 200000;
  name = "Money";
  storable = true;
  CanBeNegative = true;
 }

 ID=1; // energy
 {
  name = "energy";
 }

 ID=2; // water
 {
  name = "water";
 }

 ID=3; // waste
 {
  name = "waste";
  value = 2000;
 }
}