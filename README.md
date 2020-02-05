# facedetect
Modified code from https://docs.microsoft.com/en-us/azure/cognitive-services/face/quickstarts/csharp
Quickstart: Detect faces in an image using the Face REST API and C#

Input image containing a human face read from local disk.

JSON output to console and file

[
   {
      "faceId": "971626a9-89ee-4c6f-bd4f-6517627c9c6c",
      "faceRectangle": {
         "top": 128,
         "left": 272,
         "width": 68,
         "height": 68
      },
      "faceAttributes": {
         "smile": 0.0,
         "headPose": {
            "pitch": 6.3,
            "roll": -8.8,
            "yaw": -37.7
         },
         "gender": "female",
         "age": 23.0,
         "facialHair": {
            "moustache": 0.0,
            "beard": 0.0,
            "sideburns": 0.0
         },
         "glasses": "NoGlasses",
         "emotion": {
            "anger": 0.0,
            "contempt": 0.001,
            "disgust": 0.0,
            "fear": 0.0,
            "happiness": 0.0,
            "neutral": 0.994,
            "sadness": 0.003,
            "surprise": 0.002
         },
         "blur": {
            "blurLevel": "medium",
            "value": 0.57
         },
         "exposure": {
            "exposureLevel": "goodExposure",
            "value": 0.43
         },
         "noise": {
            "noiseLevel": "low",
            "value": 0.27
         },
         "makeup": {
            "eyeMakeup": true,
            "lipMakeup": true
         },
         "accessories": [

         ],
         "occlusion": {
            "foreheadOccluded": false,
            "eyeOccluded": false,
            "mouthOccluded": false
         },
         "hair": {
            "bald": 0.01,
            "invisible": false,
            "hairColor": [
               {
                  "color": "brown",
                  "confidence": 0.98
               },
               {
                  "color": "red",
                  "confidence": 0.98
               },
               {
                  "color": "other",
                  "confidence": 0.41
               },
               {
                  "color": "blond",
                  "confidence": 0.22
               },
               {
                  "color": "black",
                  "confidence": 0.08
               },
               {
                  "color": "gray",
                  "confidence": 0.03
               }
            ]
         }
      }
   }
]

