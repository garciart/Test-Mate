# Project Test Mate

Test Mate is a customizable, self-study system, designed to help you in achieve your educational and professional goals. Why study alone?


![Welcome to Test Mate!](Images/tm_image_01.png "Welcome to Test Mate!"){width="200px"}

## Table of Contents

- [Introduction](#introduction)
- [Part 1: Requirements Analysis](#part-1-requirements-analysis)
- [Part 2: System and Database Design](#part-2-system-and-database-design)
- [References](#references)

---

## Introduction:

Test Mate was born at a hotel bar in Singapore in 2006. Standardized testing became a part of my life when I joined the Marine Corps, and it seemed that every class and every inspection included a standardized test. Therefore, once I got my hands on a personal computer (over $2000 for a 386 in 1991!), I wrote a program that I could use to create my own practice tests. The first versions were written in BASICA with hard-coded data, but as time went on, I used different languages and added more features; such as a GUI; question-and-answer randomization; flat-file reading; and even wrote a version in dBase III that I used to print tests for my students while I was an instructor.

Eventually, while I was in the middle of the desert at Twentynine Palms, I began taking CLEP exams for college credit, and I used my as-of-yet unnamed program to study for these standardized tests. An education counselor asked me how I was achieving such high grades and I showed him the program. He told me I should take my show on the road, but I didn't give it serious thought until a few years later, when a ship I was on pulled into Singapore on my way back to California. Before I had left the States, I had purchased a XV6700 Pocket PC, and while I was walking through Singapore's Borders bookstore, I perused a book on .NET Compact Framework programming (BTW, Singapore is a technophile's dream). I didn't buy the book, but as I sat at the hotel bar waiting for friends, I decided I would attempt to port my program to my XV6700.

For many reasons, I never marketed Test Mate, but I continued to port it to different mobile operating systems, such as Apple's iOS (C++), the BlackBerry OS (Java), Palm's webOS (HTML/JavaScript), and my Android devices (which I still use to this day). Therefore, as one of my shared projects, I intend to write my version of Quizlet-before-Quizlet in JavaFX and Xamarin, and I hope you can get something out of it!

## Part 1: Requirements Analysis:

- As a user, I want to be able to take tests on tablets and mobile devices, so that I can study anywhere.
- As a user, I want to be able to take tests without an internet connection, so that I can study anywhere.
- As a user, I want to access multiple tests on a single device, so that I can switch from test to test as needed.
- As a user, I want to be able to delete tests I no longer need, so that I can save space on my device.
- As a user, I want the option to randomize both the order of the test questions and their answers, so that each testing experience is unique.
- As a user, I want the option to receive feedback after answering a question, so that I can learn as I take the test.
- As a user, I want the option to receive feedback only after I complete the test, so that I can simulate the conditions of a real test.
- As a user, I want to store multiple tests on a single device, so that I do not need to download a test each time I want to take the test.
- As a user, I want to be able to customize or create my own standardized tests, so that I can tailor test subjects to my needs.
- As a user, I want the option to use key term matching; multiple-choice; or true/false question formats when I create a test, so that I can become accustomed to the format I will most likely see on the actual test.
- As an administrator, I want to be able to have a single repository for tests, so that I can maintain the integrity of test content.
- As an administrator, I want the test repository accessible to all users, so that I can maintain test availability.
- As an administrator, I want to be able to review user tests before including them in the repository, so that I can check the quality of test content.

## Part 2: System and Database Design:

![Test Mate Class Diagram](Images/TMClassDiagram.png "Test Mate Class Diagram")

## References:
