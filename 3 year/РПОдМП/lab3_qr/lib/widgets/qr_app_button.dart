import 'package:flutter/material.dart';

class QrAppButton extends StatelessWidget {
  String? text;
  VoidCallback? onPressed;
  double? width;
  double? height;

  QrAppButton(
      {super.key, required this.text, required VoidCallback onPressed}) {
    width = 220.0;
    height = 40.0;
    this.onPressed = onPressed;
  }

  QrAppButton.sized(
      {super.key,
      required this.text,
      required this.width,
      required this.height,
      required VoidCallback onPressed}) {
    this.text = text;
    this.width = width;
    this.height = height;
    this.onPressed = onPressed;
  }

  @override
  Widget build(BuildContext context) {
    return InkWell(
      child: SizedBox(
        width: width,
        height: height,
        child: ElevatedButton(
          onPressed: onPressed,
          style: ButtonStyle(
            backgroundColor: MaterialStateProperty.all<Color>(
              const Color.fromARGB(255, 125, 205, 184),
            ),
            shape: MaterialStateProperty.all<RoundedRectangleBorder>(
              RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(9.0),
                side: const BorderSide(
                  width: 2,
                  color: Color.fromARGB(255, 95, 155, 134),
                ),
              ),
            ),
          ),
          child: Text(
            text!,
            style: const TextStyle(
              color: Color.fromARGB(255, 245, 245, 245),
              fontSize: 22,
              fontWeight: FontWeight.w400,
            ),
          ),
        ),
      ),
    );
  }
}
