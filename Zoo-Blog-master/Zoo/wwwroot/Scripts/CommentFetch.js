document.addEventListener("DOMContentLoaded", () => {
  const id = document.querySelector("p#AnimalId").innerText;
  const form = document.querySelector("form");
  const CommentsDiv = document.querySelector("div#Comments");
  const contentTextArea = document.querySelector("textarea#Content");
  const BaseUrl = "http://localhost:5063/AnimalData";
  const getComments = "GetComments";

  //init
  getAllComments();

  async function getAllComments() {
    CommentsDiv.innerHTML = "";
    try {
      const response = await fetch(`${BaseUrl}/${getComments}?id=${id}`);
      const responseType = response.headers.get("content-type");
      {
        if (responseType.includes("application/json")) {
          const comments = await response.json();
          if (comments.length > 0) {
            for (let i = 0; i < comments.length; i++) {
              const newCommentDiv = document.createElement("div");
              const newCommentTextArea = document.createElement("textarea");
              newCommentDiv.classList.add(
                "container-fluid",
                "p-2",
                "mb-1",
                "text-left"
              );
              newCommentTextArea.classList.add(
                "text-wrap",
                "bg-success",
                "text-white",
                "col-12"
              );
              newCommentTextArea.disabled = true;
              newCommentTextArea.innerText = comments[i];
              CommentsDiv.appendChild(newCommentDiv);
              newCommentDiv.appendChild(newCommentTextArea);
            }
          }
        }
      }
    } catch (err) {
      console.log(err);
    }
  }
  async function addComment(event) {
    let comment = {
      CommentId: undefined,
      Content: contentTextArea.value,
      AnimalID: id,
    };
    comment = JSON.stringify(comment);
    await fetch(`${BaseUrl}/Index`, {
      method: "POST",
      body: comment,
      headers: {
        "content-type": "application/json",
      },
    }).then(() => {
      getAllComments();
    });
  }
  form.addEventListener("submit", (event) => {
    event.preventDefault();
    addComment(event);
  });
});
