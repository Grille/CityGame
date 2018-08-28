
//area{ water, pollution, road, saltwater}

 Attributes{
  string name;
  bool smooth;
 }

 Init {
  name = "";
  smooth = true;
 }

  ID-0 {
  name = "water";
  smooth = false;
 }
  ID-1 {
  name = "saltwater";
  smooth = false;
 }

 ID-2 {
  name = "pollution";
 }

 ID-3 {
  name = "road";
  smooth = false;
 }
 ID-4 {
  name = "radiation";
 }
