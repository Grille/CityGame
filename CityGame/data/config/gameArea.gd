
//area{ water, pollution, road, saltwater}

 Attributes{
  string name;
  bool smooth;

  name = "";
  smooth = true;
 }

<water> {
  name = "water";
  smooth = false;
 }
  <saltwater> {
  name = "saltwater";
  smooth = false;
 }

 <pollution> {
  name = "pollution";
 }

 <road> {
  name = "road";
  smooth = false;
 }
 <radiation> {
  name = "radiation";
 }
