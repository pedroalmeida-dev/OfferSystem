# Offer Management System for Unity

This repository contains a modular and extensible **Personalized Offer System** for Unity, enabling dynamic activation and deactivation of offers 
based on configurable triggers and validation conditions.

## 🧩 Overview

The system allows you to:
- Manage multiple in-game offers.
- Register offers with **date-based** or **event-driven** triggers.
- Validate offers against custom business rules.
- Automatically unlock and activate chained offers upon purchase.

## 📦 Core Components

### `OfferManager`
Singleton MonoBehaviour responsible for:
- Initializing and managing offers.
- Subscribing to and handling trigger events.
- Validating and activating/deactivating offers.
- Unlocking chained offers upon purchase.

### `Offer`
Represents a purchasable in-game item/offer.
- Has a unique `Id`, associated `TransactionId`, and optional `NextOfferId`.
- Contains a list of `IOfferTrigger`s and `OfferValidationData`s.
- Chained offers can be unlocked upon purchase.

### `TriggerManager`
Handles activation and deactivation of registered triggers:
- Supports `DateTrigger` (time-based) and `EventTrigger` (custom game events).
- Periodically checks and updates trigger status.

### `ValidationManager`
Ensures an offer meets all custom `IValidationCondition` rules before activation.

### Trigger Types:
- **`DateTrigger`**: Becomes active based on time intervals.
- **`EventTrigger`**: Fires when a named event is triggered in-game.

### Interfaces:
- `IOfferTrigger`: Defines trigger lifecycle.
- `IValidationCondition`: Represents a validation rule for offers.

## 🧪 Example Usage

```csharp
OfferManager.Instance.Initialize(
    offers: new List<Offer> { myOffer1, myOffer2 },
    validationManager: new ValidationManager(new MyCustomValidationCondition())
);

// Manually trigger an event
OfferManager.Instance.TriggerManager.FireTrigger("LEVEL_COMPLETE");

// Notify system of a purchase
OfferManager.Instance.OnOfferPurchased("offer_001");

// Get currently active offers
List<Offer> active = OfferManager.Instance.GetActiveOffers();
```

## 🔄 Offer Lifecycle

1. **Initialization**:
   - Offers and validation conditions are registered.
   - Triggers are linked to offers.

2. **Activation**:
   - If all triggers for an offer are active AND it passes all validations, it is activated.
   - `OnOfferActivated` event is fired.

3. **Deactivation**:
   - When a trigger ends (e.g., time expires), the offer is deactivated.
   - `OnOfferDeactivated` event is fired.

4. **Purchase Flow**:
   - Upon purchase, the offer is removed from the active list.
   - If a `NextOfferId` is defined, the next offer is activated.

## ⚙️ Extending the System

### Add New Validation Logic
Implement `IValidationCondition`:
```csharp
public class PlayerLevelCondition : IValidationCondition {
    public string Id => "player_level";

    public bool IsValid(string key, float value) {
        return Player.Level >= value;
    }
}
```

### Add New Trigger Types
Implement `IOfferTrigger` to create custom trigger logic.

## 📂 File Structure

```
Runtime/
    OfferSystem.asmdef
    OfferManager.cs
    Offer.cs
    OfferValidationData.cs
    TriggerManager.cs
    ValidationManager.cs
    IValidationCondition.cs
    Triggers/
        DateTrigger.cs
        EventTrigger.cs
        IOfferTriggercs 
        TriggerManager.cs
README.md
CHANGELOG.md
package.json
```
