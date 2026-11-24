003

Status - Waiting for Approval

Context - Implementing tests with editor window

Decision:
This adr overrides 001-hranice-modulov.md
From now on Editor module can communicate with Tests module.

Consequences:
We will be able to call functions from Tests module in Editor scripts.
F.E WavesValidationWindow will be able to call WavesValidator.Validate() when we press a button.