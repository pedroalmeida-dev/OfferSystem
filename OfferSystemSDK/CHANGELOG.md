# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.0.1]
### Added
- Initial release of Offer Management System for Unity.
- `OfferManager` class to manage offer lifecycles.
- `Offer` class representing in-game purchasable offers.
- `TriggerManager` for handling `DateTrigger` and `EventTrigger`.
- `ValidationManager` for validating offers against custom conditions.
- Interfaces: `IOfferTrigger` and `IValidationCondition`.
- Offer chaining through `NextOfferId`.
- Event-based activation/deactivation system.
