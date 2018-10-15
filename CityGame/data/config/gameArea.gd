
//area{ water, pollution, road, saltwater}

 Attributes{
  string name;
  bool smooth;
 }

 Init {
  name = "";
  smooth = true;
 }

<0> {
  name = "water";
  smooth = false;
 }
  <1> {
  name = "saltwater";
  smooth = false;
 }

 <2> {
  name = "pollution";
 }

 <3> {
  name = "road";
  smooth = false;
 }
 <4> {
  name = "radiation";
 }
