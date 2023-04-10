import { gql } from '@apollo/client';

export const ADD_ITEM = gql(`
    mutation add_item($text: String!) {
        add_item(text: $text)
    }
`);

export const CHECK_ITEM = gql(`
    mutation check_item($id: String!) {
        check_item(id: $id)
    }
`);

export const DELETE_ITEM = gql(`
    mutation delete_item($id: String!) {
        delete_item(id: $id)
    }
`);