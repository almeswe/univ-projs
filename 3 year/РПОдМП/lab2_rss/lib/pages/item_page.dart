import 'package:webfeed/webfeed.dart';
import 'package:flutter/material.dart';

import 'package:lab2_rss/rss/lifehacker_rss_model.dart';

class RssReaderItemPage extends StatelessWidget {
  RssItem item;
  RssReaderItemPage({super.key, required this.item});

  @override
  Widget build(BuildContext context) {
    var model = LifehackerItemModel(item);
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      theme: ThemeData(primaryColor: const Color.fromRGBO(58, 66, 86, 1.0)),
      home: Scaffold(
        body: FutureBuilder(
          future: model.fetchItem(),
          builder: (context, snapshot) {
            if (!snapshot.hasData) {
              return const Center(
                child: CircularProgressIndicator(),
              );
            } else {
              return ListView(
                children: [
                  Text(
                    model.title,
                    style: const TextStyle(
                      fontSize: 25,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  Text(
                    model.subtitle,
                    style: const TextStyle(
                      fontSize: 20,
                      fontWeight: FontWeight.w400,
                    ),
                  ),
                  for (var paragraph in model.paragraphs)
                    Column(
                      children: [
                        Padding(
                          padding: const EdgeInsets.only(top: 15),
                          child: Text(
                            paragraph.header,
                            style: const TextStyle(
                              fontSize: 23,
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ),
                        for (var content in paragraph.contents)
                          Padding(
                            padding: const EdgeInsets.only(top: 10),
                            child: Text(
                              content,
                              style: const TextStyle(
                                fontSize: 17,
                                fontWeight: FontWeight.w400,
                              ),
                            ),
                          ),
                      ],
                    ),
                ],
              );
            }
          },
        ),
      ),
    );
  }
}
