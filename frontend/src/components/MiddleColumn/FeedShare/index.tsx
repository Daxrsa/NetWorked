import React, { useState } from "react";
import Panel from "../../Panel";
import {
  Container,
  WriteIcon,
  CameraIcon,
  VideoCameraIcon,
  DocumentIcon,
  ArticleIcon,
} from "./styles";
import { MDBBtn } from "mdb-react-ui-kit";
import { Button } from "@mui/material";
import AddPostForm from "./AddPostForm";
import DoDisturbIcon from "@mui/icons-material/DoDisturb";

const FeedShare: React.FC = () => {
  const [idAddMode, setAddMode] = useState(false);
  const openAddForm = () => {
    setAddMode(true);
  };
  const closeForm = () => {
    setAddMode(false);
  };
  return (
    <Panel>
      <Container>
        <div className="write">
          <WriteIcon />
          {idAddMode ? (
            <AddPostForm />
          ) : (
            <Button variant="text" color="secondary" onClick={openAddForm}>
              Add Post
            </Button>
          )}
          <hr />
        </div>
        {idAddMode ? (
            <Button onClick={closeForm}>Cancel</Button>
        ) : (
          <></>
        )}
        <div className="attachment">
          <button>
            <CameraIcon />
            Photo
          </button>
          <button>
            <VideoCameraIcon />
            Video
          </button>
          <button>
            <DocumentIcon />
            Document
          </button>
          <button>
            <ArticleIcon />
            Write article
          </button>
        </div>
      </Container>
    </Panel>
  );
};

export default FeedShare;
