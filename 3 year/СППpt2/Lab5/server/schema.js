const schema = require('graphql').buildSchema( 
`
  type Item {
    id: String!
    text: String!
    isChecked: Boolean!
  }

  type Query {
    get_items: [Item]
  }

  type Mutation {
    add_item(text: String!): Int
    check_item(id: String): Int
    delete_item(id: String): Int
  }
`);

module.exports = schema;