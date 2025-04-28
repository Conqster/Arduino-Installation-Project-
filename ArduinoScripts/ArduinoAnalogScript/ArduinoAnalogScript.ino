// #define outputA 6
// #define outputB 7

// #define outputC 4
// #define outputD 5

// #define SW 8 //rotatary encoder button

// const int rotatary1MagnetA = 6;
// const int rotatary1MagnetB = 7;
#define rotatary1MagnetA  6
#define rotatary1MagnetB  7
const int rotatary1Button = 8;
const int rotatary2MagnetA = 4;
const int rotatary2MagnetB = 5;

//Ultrasonic sensors
const int trigPinUS = 10; //trig pin for Ultrasonic1
const int echoPinUS = 11; //echo pin for Ultrasonic1
long durationUS; //duration for Ultrasonic 1
int distanceUS; // distance fro Ultrasonic 1
const int trigPinUS2 = 12; //trig pin for Ultrasonic 2
const int echoPinUS2 = 13; //echo pin for Ultrasonic 2
long durationUS2; //duration for Ultrasonic 2
int distanceUS2; // distance fro Ultrasonic 2

//Led lights
const int miniGame1Light = 20;
const int miniGame2Light = 21;
const int miniGame3Light = 22;

//Buttons for miniGames
const int miniGame1Button = 23;
const int miniGame2Button = 24;
const int miniGame3Button = 25;

int counter = 0;
int aState;
int aLastState;
// iebvijvbevb
int counter2=0;
int aState2;
int aLastState2;

long timer1;
long timer2;
// unsigned long timer2 = milli();
// unsigned long timer3 = milli();
// unsigned long timer4 = milli();

void setup() 
{
  pinMode(rotatary1MagnetA, INPUT);
  pinMode(rotatary1MagnetB, INPUT);  

  pinMode(rotatary2MagnetA, INPUT);
  pinMode(rotatary2MagnetB, INPUT);
	
  pinMode(rotatary1Button, INPUT_PULLUP);

  pinMode(trigPinUS, OUTPUT);
  pinMode(echoPinUS, INPUT);

  pinMode(trigPinUS2, OUTPUT);
  pinMode(echoPinUS2, INPUT);

  
  // initialize serial communication at 9600 bits per second:
  Serial.begin(9600);
  aLastState = digitalRead(rotatary1MagnetA);
  aLastState2 = digitalRead(rotatary2MagnetA);
}

// the loop routine runs over and over again forever:
void loop()
{
  int btnState = digitalRead(rotatary1Button);
  
  //RotataryEncoder1();
  // Ultrasonic();
  // Ultrasonic2();

  aState = digitalRead(rotatary1MagnetA);
  if(aState!=aLastState)
  {
    if(digitalRead(rotatary1MagnetB)!=aState)
    {
      counter ++;
    }
    else 
    {
      counter --;
    }
    
  }
  aLastState = aState;
  
  RotataryEncoder2();
  
  Serial.print(counter);
  Serial.print(",");
  Serial.print(counter2);
  Serial.print(",");
  Serial.print(counter3);
  Serial.print(",");
  Serial.print(rotary3ButtonState);
  Serial.print(",");
  Serial.print(ultrasonic1Distance);
  Serial.print(",");
  Serial.print(ultrasonic2Distance);
  Serial.print(",");
  Serial.println(buttonAction);

  
  // aLastState2 = aState2;

  // // read the input on analog pin 0:
  // int sensorValue = analogRead(A0);
  // // print out the value you read:

  // Serial.println(sensorValue);
  // delay(1);  // delay in between reads for stability
}



void RotataryEncoder1()
{
  aState = digitalRead(rotatary1MagnetA);
  if(aState!=aLastState){

    if(digitalRead(rotatary1MagnetB)!=aState){

      counter++;

    }

    else {
      
      counter--;

    }

  }
  aLastState = aState;
}

void RotataryEncoder2()
{
  
  aState2=digitalRead(rotatary2MagnetA);
  if(aState2!=aLastState2){

    if(digitalRead(rotatary2MagnetB)!=aState2){

      counter2++;

    }

    else {
      
      counter2--;

    }

  }
  aLastState2 = aState2;
}

void Ultrasonic()
{
  timer1 = (timer1 + millis());
  if(timer1 > 2)
  {
    //delayMicroseconds(2); // Sets the trigPin on HIGH state for 10 micro seconds
    digitalWrite(trigPinUS, HIGH);
    //delayMicroseconds(10);
    if(timer1 > 12)
    {
      digitalWrite(trigPinUS, LOW); // Reads the echoPin, returns the sound wave travel time in microseconds
      durationUS = pulseIn(echoPinUS, HIGH);
      distanceUS = durationUS*0.034/2; // Calculating the distance
      timer1 = 0;

    }
  }
}

void Ultrasonic2()
{
  digitalWrite(trigPinUS2, LOW); // Clears the trigPin
  timer2 += millis();
  if(timer2 > 2)
  {
    delayMicroseconds(2); // Sets the trigPin on HIGH state for 10 micro seconds
    digitalWrite(trigPinUS2, HIGH);

    if(timer2 > 12)
    {
      digitalWrite(trigPinUS2, LOW); // Reads the echoPin, returns the sound wave travel time in microseconds
      durationUS2 = pulseIn(echoPinUS2, HIGH);
      distanceUS2 = durationUS2*0.034/2; // Calculating the distance
      timer2 = 0;
    }

  }
}




