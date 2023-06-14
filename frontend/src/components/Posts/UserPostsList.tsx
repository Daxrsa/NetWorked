import React, { useEffect, useState } from "react";
import { Button, Item, Segment } from "semantic-ui-react";
import { Post } from "../../models/Post";

interface Props {
    posts: Post[];
  }

export default function UserPostsList({posts}: Props){
    return (
        <Segment>
            <Item.Group divided>
                {posts.map(post => (
                    <Item key={post.id}>
                        <Item.Content>
                            <Item.Header as='a'>{post.username}</Item.Header>
                            <Item.Header>{post.title}</Item.Header>
                            <Item.Description>
                                <div>
                                    {post.description}
                                </div>
                            </Item.Description>
                            <Item.Meta>{post.dateCreated}</Item.Meta>
                            <Item.Meta>{post.likes}</Item.Meta>
                            <Item.Extra>
                                <Button floated="right" content="View" color="blue" />
                            </Item.Extra>
                        </Item.Content>
                    </Item>
                ))}
            </Item.Group>
        </Segment>
    )
}