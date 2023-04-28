const { Router } = require("express");
const authMiddleware = require("../middlewares/auth");

const router = Router();

router.get("/", authMiddleware, (req, res) => {
  res.status(200).json(res.user);
});

module.exports = router;