## 🎯 Offer System SDK

**Purpose**: In-Game Personalized Offer Management  
**Technologies**: Unity, C#  
**Development Time**: 8 hours (Technical Challenge)  
**Team Size**: 1  

**Description**:  
This Offer System SDK provides a modular and extensible framework for managing personalized in-game offers in Unity. It enables dynamic activation and deactivation of offers based on both time and event-based triggers, while also supporting chained offers and custom validation logic.

**Architecture**:  
- Singleton-based `OfferManager` orchestrates offer lifecycle and purchases  
- Event-driven and time-based triggering via `TriggerManager`  
- Rule-based validation system using `IValidationCondition` interface  
- Offers support chaining: unlock new offers upon purchase  
- Designed for flexibility and extensibility through interfaces and decoupled components  

**Highlights**:
- Built clean C# architecture with clear separation of concerns  
- Implemented real-time trigger system (event- and time-based)  
- Designed flexible validation logic for business rules  
- Supported chained offer progression and purchase flow  
- Packaged as a Unity SDK with assembly definitions and modular folder structure  

**My Role**:  
I designed and developed the full Offer System from scratch in under 8 hours, demonstrating my ability to architect scalable gameplay systems quickly. I focused on clean API design, real-time triggering, and robust validation workflows suitable for integration in a live mobile game.
