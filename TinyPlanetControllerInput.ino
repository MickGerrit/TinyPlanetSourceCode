#include <MPU6050_tockn.h>
#include <Wire.h>
#include <SoftwareSerial.h>

SoftwareSerial btSerial(8,9);

float angleX;
float angleY;
float angleZ;

String angles;
String buttons;
String mic;
String vibration;
String outputString;

MPU6050 mpu6050(Wire, 0.0, 1.0);

const int Button0 = 10;
const int Button2 = 11;
const int Button1 = 12;
const int Button3 = 13;

const int vibrationOut = 6;

const int soundPin = A3;
float vibrationAmount = 0;

int resetTimeLeft;
int soundVal = 0;

int myTimeout = 5;

void setup() {
  Serial.begin(115200);
  btSerial.begin(115200);
  btSerial.setTimeout(myTimeout);
  Wire.begin();

  pinMode(Button0, INPUT);
  pinMode(Button1, INPUT);
  pinMode(Button2, INPUT);
  pinMode(Button3, INPUT);
  
  mpu6050.begin();
  mpu6050.calcGyroOffsets(true);
  /*mpu6050.setGyroOffsets(-1.09, -0.09, 0.61);*/ /* Reset the gyro with this function*/
  int resetTimeLeft = 0;
  
  vibrationAmount = 0.0;/*between 100.0 and 255.0*/
}


void loop() {                    
  mpu6050.update();
    
  resetTimeLeft += 1;
  if (resetTimeLeft > 40){
    resetTimeLeft = 0;
  }
    
  soundVal = analogRead(soundPin);
    
  angleX = mpu6050.getAngleX();
  angleY = mpu6050.getAngleY();
  angleZ = mpu6050.getAngleZ();
  angles = "x" +String(angleX) +"y"+String(angleY) +"z"+String(angleZ);
  buttons = "a" + String(digitalRead(Button0)) + "b" + String(digitalRead(Button1)) + "c0" + "d0";
  mic = "s" + String(soundVal);
  vibration = "v"+String(vibrationAmount);

  
  outputString = btSerial.readStringUntil('\n');
  
  if (outputString.startsWith("v", 0)){
      vibrationAmount = (outputString.substring(1)).toFloat();
      writeString("Vibration set to: "); 
      writeString(vibration);
      btSerial.println("");  
    }
  writeString(angles+buttons+mic+vibration);
  btSerial.println("");
  analogWrite(vibrationOut,vibrationAmount);
  delay(70);
  analogWrite(vibrationOut,0);
  btSerial.flush();
//    for (int i = 100; i < 255; i++){ //if i is less than 255 then increase i with 1
//    analogWrite(vibrationOut, i); //write the i value to pin 11
//    delay(5); //wait 5 ms then do the for loop again
//  }
//  for (int i = 255; i > 100; i--){ //descrease i with 1
//    analogWrite(vibrationOut, i);
//    delay(5);
//  }
  }

  double mapf(double val, double in_min, double in_max, double out_min, double out_max) {
    return (val - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
  }

  void writeString(String stringData) { // Used to software serially push out a String with btSerial.write()

  for (int i = 0; i < stringData.length(); i++)
  {
    btSerial.write(stringData[i]);   // Push each char 1 by 1 on each loop pass
  }

}// end writeString
