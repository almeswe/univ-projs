const { Router } = require("express");
const AuthController = require("../controllers/auth");
const authMiddleware = require("../middlewares/auth");

const authController = new AuthController();
const router = Router();

router.post("/signin", authController.signin);
router.post("/signup", authController.signup);

router.get("/home", authMiddleware, (req, res) => {
    res.status(200).json("authorized");
});

module.exports = router;