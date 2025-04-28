// <string.h>
#define rotary1A 2
#define rotary1B 3
#define rotary2A 4
#define rotary2B 5
#define rotary3A 6
#define rotary3B 7
#define rotary3Button 8
#define button1 9
#define button2 10
#define button3 11
#define light1R 12
#define light1G 13
#define light1B 14
#define light2R 15 
#define light2G 16
#define light2B 17
#define light3R 18
#define light3G 19
#define light3B 20
#define ultrasonic1Trig 22
#define ultrasonic1Echo 23
#define ultrasonic2Trig 24
#define ultrasonic2Echo 25

enum GameState { mainMenu, puzzuleGame, boatGame, shootingGame};
GameState gameState = GameState::mainMenu;

int counter = 0;
int currentStateCLK;
int previousStateCLK;

int counter2 = 0;
int currentStateCLK2;
int previousStateCLK2;

int counter3 = 0;
int currentStateCLK3;
int previousStateCLK3;
int rotary3ButtonState;

long ultrasonic1Timer;
long ultrasonic1Duration;
int ultrasonic1Distance;

long ultrasonic2Timer;
long ultrasonic2Duration;
int ultrasonic2Distance;

int levelButton = 0;
int ledTime = 0;
bool buttonPressed = false;
bool buttonAction = false;

String data;
bool action;

void setup() {
  // put your setup code here, to run once:
  pinMode(rotary1A, INPUT); 
  pinMode(rotary1B, INPUT);
  pinMode(rotary2A, INPUT);
  pinMode(rotary2A, INPUT);
  pinMode(rotary2B, INPUT);
  pinMode(rotary3A, INPUT);
  pinMode(rotary3B, INPUT);
  pinMode(rotary3Button, INPUT_PULLUP);
  pinMode(button1, INPUT_PULLUP);
  pinMode(button2, INPUT);
  pinMode(button3, INPUT);
  pinMode(light1R, OUTPUT);
  pinMode(light1B, OUTPUT);
  pinMode(light1G, OUTPUT);
  pinMode(light2R, OUTPUT);
  pinMode(light2B, OUTPUT);
  pinMode(light2G, OUTPUT);
  pinMode(light3R, OUTPUT);
  pinMode(light3B, OUTPUT);
  pinMode(light3G, OUTPUT);
  pinMode(ultrasonic1Trig, OUTPUT);
  pinMode(ultrasonic1Echo, INPUT);
  pinMode(ultrasonic2Trig, OUTPUT);
  pinMode(ultrasonic2Echo, INPUT);

  Serial.begin(9600);
  previousStateCLK = digitalRead(rotary1A);
  previousStateCLK2 = digitalRead(rotary2A);
  previousStateCLK3 = digitalRead(rotary3A);
}

void loop() {
  // put your main code here, to run repeatedly:

  //gameState = (GameState)levelButton;
  ReceiveUnityData();
  
  switch(gameState)
  {
    case GameState::mainMenu:
      MainMenuControl();
      Light1(255, 0, 0);
      Light2(255, 0, 0);
      Light3(255, 0, 0);
    break;
    case GameState::puzzuleGame:
      MazeGameControl();
      Light1(0, 255, 0);
      Light2(255, 0, 0);
      Light3(255, 0, 0);
    break;
    case GameState::boatGame:
      BoatGameControl();
      Light1(0, 255, 0);
      Light2(0, 255, 0);
      Light3(255, 0, 0);
    break;
    case GameState::shootingGame:
    ShootingGameControl();
      Light1(0, 255, 0);
      Light2(0, 255, 0);
      Light3(0, 255, 0);
      levelButton = 0;
    break;
  }

    Sender(counter, counter2, counter3, rotary3ButtonState, ultrasonic1Distance, ultrasonic2Distance, buttonAction);  

}

void Sender(int m1, int m2, int m3, int m4, int m5, int m6, int m7)
{

  switch(gameState)
  {
    case GameState::mainMenu:
      Serial.print(m7);
      Serial.print(",");
      Serial.print(0);    //rotary encoder 3 button
      Serial.print(",");
      Serial.print(0); 
    break;
    case GameState::puzzuleGame:
      Serial.print(m1);   //rotary encoder 1 serial output
       Serial.print(",");
      Serial.print(0);   
      Serial.print(",");
      Serial.print(0); 
    break;
    case GameState::boatGame:
      Serial.print(m5); // ultrasonic 1 serial output
      Serial.print(",");
      Serial.print(m6); // ultrasonic 2 serial output
      Serial.print(",");
      Serial.print(0); 
    break;
    case GameState::shootingGame:
      Serial.print(m2);   // rotary encoder 2 serial output 
      Serial.print(",");
      Serial.print(m4);    //rotary encoder 3 button
      Serial.print(",");
      Serial.print(m3); 
    break;
  }
  Serial.println(",");
}

void ReceiveUnityData()
{
    if(Serial.available() > 0)
    {
      char c = Serial.read();

      switch(c)
      {
        case 'M':
          gameState = GameState::mainMenu;
          break;
        case 'B':
          gameState = GameState::boatGame;
          break;
        case 'P':
          gameState = GameState::puzzuleGame;
          break;
        case 'S':
          gameState = GameState::shootingGame;
          break;
      }
      // if(c = 'M')
      // {
      //   gameState = GameState::shootingGame;
      // }
      // if(c = 'B')
      // {
      //   gameState = GameState::boatGame;
      // }
      // if(c = 'P')
      // {
      //   gameState = GameState::puzzuleGame;
      // }
      // if(c = 'S')
      // {
      //   gameState = GameState::mainMenu;
      // }
    }



}


void MainMenuControl()
{
  bool currentState = digitalRead(button1);
  if(currentState == buttonPressed)
  {
    buttonAction = true;
    // while(digitalRead(button1) == buttonPressed){

    // }
  }
  else
  {
    buttonAction = false;
  }
}

void MazeGameControl()
{
  RotaryEncoder1();
}
void BoatGameControl()
{
  Ultrasonic1();
  Ultrasonic2();
}
void ShootingGameControl()
{
  RotaryEncoder2();
  RotaryEncoder3();
}

void Light1(int red, int green, int blue)
{
  digitalWrite(light1R, red);
  digitalWrite(light1G, green);
  digitalWrite(light1B, blue);
}

void Light2(int red, int green, int blue)
{
  digitalWrite(light2R, red);
  digitalWrite(light2G, green);
  digitalWrite(light2B, blue);
}

void Light3(int red, int green, int blue)
{
  digitalWrite(light3R, red);
  digitalWrite(light3G, green);
  digitalWrite(light3B, blue);
}


void RotaryEncoder1()
{
  currentStateCLK = digitalRead(rotary1A);
  if(currentStateCLK != previousStateCLK)
  {
    if(digitalRead(rotary1B) != currentStateCLK)
    {
      counter--;
    }
    else
    {
      counter++;
    }
  }
  previousStateCLK = currentStateCLK;
}

void RotaryEncoder2()
{
  currentStateCLK2 = digitalRead(rotary2A);
  if(currentStateCLK2 != previousStateCLK2)
  {
    if(digitalRead(rotary2B) != currentStateCLK2)
    {
      counter2--;
    }
    else
    {
      counter2++;
    }
  }
  previousStateCLK2 = currentStateCLK2;
}


void RotaryEncoder3()
{
  rotary3ButtonState = digitalRead(rotary3Button);
  currentStateCLK3 = digitalRead(rotary3A);
  if(currentStateCLK3 != previousStateCLK3)
  {
    if(digitalRead(rotary3B) != currentStateCLK3)
    {
      counter3--;
    }
    else
    {
      counter3++;
    }
  }
  previousStateCLK3 = currentStateCLK3;
}


void Ultrasonic1()
{
    digitalWrite(ultrasonic1Trig, LOW);
    delayMicroseconds(2);
    digitalWrite(ultrasonic1Trig, HIGH);
    delayMicroseconds(10);
    digitalWrite(ultrasonic1Timer, LOW);
    ultrasonic1Duration = pulseIn(ultrasonic1Echo, HIGH);
    ultrasonic1Distance = ultrasonic1Duration * 0.034 / 2;
}

void Ultrasonic2()
{
    digitalWrite(ultrasonic2Trig, LOW);
    delayMicroseconds(2);
    digitalWrite(ultrasonic2Trig, HIGH);
    delayMicroseconds(10);
    digitalWrite(ultrasonic2Trig, LOW);
    ultrasonic2Duration = pulseIn(ultrasonic2Echo, HIGH);
    ultrasonic2Distance = ultrasonic2Duration * 0.034 /2;
}
