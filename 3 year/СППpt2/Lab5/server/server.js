const cors    = require('cors');
const schema  = require('./schema');
const express = require('express');
const graphql = require('express-graphql');

const { v4: uuidv4 } = require('uuid');

const app = express();
app.use(cors());
app.use(express.json());

const items = require('./db');

const root = {
    get_items: () => {
        return items;
    },
    add_item: ({text}) => {
        const item = {
            id: uuidv4(),
            text: text,
            isChecked: false
        }
        items.push(item);
        return 200;
    },
    check_item: ({id}) => {
        const index = items.findIndex(
            item => item.id == id);
        if (index >= 0) {
            items[index].isChecked = !items[index].isChecked;
            return 200;
        }
        return 400;
    },
    delete_item: ({id}) => {
        const index = items.findIndex(
            item => item.id == id);
        if (index >= 0) {
            items.splice(index, 1);
            return 200;
        } 
        return 400;
    }
};

app.use('/graphql', graphql.graphqlHTTP({
    graphiql: true,
    schema,
    rootValue: root
}));

app.listen(3001, () => {
    console.log('server started!');
});