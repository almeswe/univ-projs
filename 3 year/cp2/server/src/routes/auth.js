const { Router } = require("express");
const AuthController = require("../controllers/auth");

const authController = new AuthController();
const router = Router();

router.post("/signin", authController.signin);
router.post("/signup", authController.signup);
router.post("/refresh", authController.refresh);

module.exports = router;