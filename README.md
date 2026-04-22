# MindfulDrinkingTracker
Unity mobile application focused on mindful alcohol consumption and habit control, built with clean UI architecture and event-driven design.

# 🍷 Mindful Drinking App (Unity)

A mobile application built in Unity focused on helping users control alcohol consumption in a healthy, non-punitive way.

## 💡 Concept

Unlike traditional habit trackers that punish failure, this app encourages:
- mindful decisions
- controlled behavior
- self-awareness instead of strict abstinence

Users can:
- start a drinking session
- track drinks in real time
- stay within a defined limit
- reflect on their behavior

## 🧱 Architecture

The project focuses heavily on clean UI architecture in Unity:

- Separation of concerns (UI vs logic)
- Event-driven UI updates
- Manual dependency injection (no MonoBehaviours in core logic)
- Screen-based navigation system

### Key Components:
- `SessionService` – core business logic
- `ScreenManager` – UI navigation
- `UIControllers` – per-screen UI logic
- Event system for decoupled communication

## 🎯 Goals

- Practice clean architecture in Unity
- Build a mobile-ready UI system
- Create a meaningful, real-world tool

## 🚀 Future Features

- Smart reminders (hydration, pacing)
- Mini cognitive tests before ordering another drink
- Soft streak system (non-punitive tracking)
- Notifications support

## 🛠 Tech

- Unity (2D / UI)
- C#
- TextMeshPro
