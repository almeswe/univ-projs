import 'package:flutter/material.dart';
import 'package:webfeed/webfeed.dart';

import 'package:lab2_rss/pages/item_page.dart';
import 'package:lab2_rss/rss/lifehacker_rss_reader.dart';

class RssReaderHomePage extends StatefulWidget {
  const RssReaderHomePage({super.key});

  @override
  _RssReaderHomeState createState() {
    return _RssReaderHomeState();
  }
}

class _RssReaderHomeState extends State {
  void _rssItemOpen(BuildContext context, RssItem item) {
    Navigator.push(
      context,
      MaterialPageRoute(
        builder: (context) {
          return RssReaderItemPage(item: item);
        },
      ),
    );
  }

  Widget _rssItemBuilder(BuildContext context, int index) {
    var item = LifehackerReader.getItem(index);
    var time = item.pubDate!.toLocal();
    return Card(
      elevation: 8.0,
      margin: const EdgeInsets.symmetric(
        horizontal: 10.0,
        vertical: 6.0,
      ),
      child: Container(
        decoration: const BoxDecoration(
          color: Color.fromRGBO(64, 75, 96, .9),
        ),
        child: ListTile(
          title: Text(
            item.title!,
            style: const TextStyle(
              color: Colors.white,
              fontWeight: FontWeight.w300,
            ),
          ),
          subtitle: Column(
            children: [
              Row(
                children: <Widget>[
                  const Icon(
                    Icons.access_time,
                    color: Colors.white,
                    size: 15,
                  ),
                  Text(
                    " ${time.year}-${time.month}-${time.day} ${time.hour}:00",
                    style: const TextStyle(
                      color: Colors.white,
                    ),
                  ),
                ],
              ),
            ],
          ),
          trailing: IconButton(
            icon: const Icon(
              Icons.arrow_right,
              size: 35,
            ),
            onPressed: () {
              _rssItemOpen(context, item);
            },
          ),
          contentPadding: const EdgeInsets.symmetric(
            horizontal: 20.0,
            vertical: 10.0,
          ),
        ),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      theme: ThemeData(primaryColor: const Color.fromRGBO(58, 66, 86, 1.0)),
      home: Scaffold(
        backgroundColor: const Color.fromRGBO(58, 66, 86, 1.0),
        appBar: AppBar(
          title: const Center(
            child: Text('Lifehacker Feed'),
          ),
          backgroundColor: const Color.fromRGBO(58, 66, 86, 1.0),
        ),
        body: FutureBuilder(
          future: LifehackerReader.fetchItems(),
          builder: (context, snapshot) {
            if (!snapshot.hasData) {
              return const Center(
                child: CircularProgressIndicator(),
              );
            } else {
              return ListView.builder(
                scrollDirection: Axis.vertical,
                padding: const EdgeInsets.only(left: 5, right: 5, bottom: 5),
                itemCount: LifehackerReader.getItemCount(),
                itemBuilder: _rssItemBuilder,
              );
            }
          },
        ),
      ),
    );
  }
}
