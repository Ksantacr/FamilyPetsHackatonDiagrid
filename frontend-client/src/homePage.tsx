// Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
// SPDX-License-Identifier: Apache-2.0

import { Button, DropZone, Flex, Heading, Text, View, VisuallyHidden } from '@aws-amplify/ui-react';
import { useRef, useState } from 'react';
import Modal from 'react-responsive-modal';
import { useNavigate } from 'react-router-dom';
import 'react-responsive-modal/styles.css';
import axios from 'axios';


/*eslint-disable*/
function parseJwt (token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
    return JSON.parse(jsonPayload);
}

const HomePage = () => {
  const navigate = useNavigate();
  var idToken = parseJwt(sessionStorage.idToken.toString());
  var accessToken = parseJwt(sessionStorage.accessToken.toString());
  const handleLogout = () => {
    sessionStorage.clear();
    navigate('/login');
  };

  const [selectedFile, setSelectedFile] = useState(null);
  const acceptedFileTypes = ['image/png', 'image/jpeg'];


  const handleFileChange = (event) => {
    setSelectedFile(event.target.files[0]);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();

    const formData = new FormData();
    formData.append('image', selectedFile);

    const apiUrl = process.env.REACT_APP_API_URL; // Get URL from environment variable

    try {
      await axios.post(apiUrl, formData, {
        headers: {
          'Content-Type': 'multipart/form-data'
        }
      });
      // Handle success (e.g., show a success message, clear form)
      console.log('Pet lost report submitted successfully!');
    } catch (error) {
      // Handle errors (e.g., display an error message)
      console.error('Error submitting pet lost report:', error);
    }
  };

  const [open, setOpen] = useState(false);

  const onOpenModal = () => setOpen(true);
  const onCloseModal = () => setOpen(false);

  const [files, setFiles] = useState([]);
  const hiddenInput = useRef(null);

  const onFilePickerChange = (event) => {
    const { files } = event.target;
    if (!files || files.length === 0) {
      return;
    }
    setFiles(Array.from(files));
  };

/*eslint-enable*/

  return (
    <div>
      <div className='mb-5'>
          <img src="./familypet.png" width={200} />
        </div>       
    <View margin="2rem">
                    <Heading level={3}>Welcome to PetFamily!</Heading>
                    <Flex direction="row" justifyContent="space-around">
                        <Button variation="primary" onClick={() => true}>
                            Report Lost Pet
                        </Button>
                        <Button variation="primary" onClick={() => true}>
                            Help Find a Pet
                        </Button>
                    </Flex>

                    <button onClick={onOpenModal}>About Us</button>

                    <button onClick={handleLogout}>Logout</button>

                    {/* About Us Modal */}
                    <Modal open={open} onClose={onCloseModal} center>
                      <h2>Family Pets</h2>
                      <p>Our mission is to assist in the search for lost pets with families through a seamless, transparent and compassionate process using Diagrid Catalyst.</p>
                      <div>
                        <h4>Supported Breeds byt the ML model on Rekognition:</h4>
                        <Flex direction="row">
                          <ul>
                          <li>abyssinian</li>
                          <li>american_bulldog</li>
                          <li>american_pit_bull_terrier</li>
                          <li>basset_hound</li>
                          <li>beagle</li>
                          <li>bengal</li>
                          <li>birman</li>
                          <li>bombay</li>
                          <li>boxer</li>
                          <li>keeshond</li>
                          <li>leonberger</li>
                          <li>maine_coon</li>
                          <li>miniature_pinscher</li>
                          <li>newfoundland</li>
                          </ul>

                          <ul>
                          
                          <li>persian</li>
                          <li>pomeranian</li>
                          <li>pug</li>
                          <li>ragdoll</li>
                          <li>russian_blue</li>
                          <li>saint_bernard</li>
                          <li>samoyed</li>
                          <li>scottish_terrier</li>
                          <li>shiba_inu</li>
                          <li>siamese</li>
                          <li>sphynx</li>
                          <li>staffordshire_bull_terrier</li>
                          <li>wheaten_terrier</li>
                          <li>yorkshire_terrier</li>
                        </ul>
                        </Flex>
                        
                      </div>
                      <a href='https://github.com/Ksantacr/FamilyPetsHackatonDiagrid/' target="_blank">Github repository</a>
                    </Modal>
                </View>

                <View>
                  {/* <form>
                  <Input placeholder="Baggins"/>
                  </form> */}
                   <form onSubmit={handleSubmit}>
      <div>
        <label htmlFor="image">Pet Image:</label>
        {/* <input type="file" id="image" onChange={handleFileChange} /> */}

        <>
      <DropZone
        acceptedFileTypes={acceptedFileTypes}
        onDropComplete={({ acceptedFiles, rejectedFiles }) => {

          console.log(acceptedFiles)
          setFiles(acceptedFiles);
        }}

        onDrop={()=> console.log("on drop")}

      >
        <Flex direction="column" alignItems="center">
          <Text>Drag pet photo here or</Text>
          <Button size="small" onClick={() => hiddenInput.current.click()}>
            Browse
          </Button>
        </Flex>
        <VisuallyHidden>
          <input
            type="file"
            tabIndex={-1}
            ref={hiddenInput}
            onChange={onFilePickerChange}
            multiple={false}
            accept={acceptedFileTypes.join(',')}
          />
        </VisuallyHidden>
      </DropZone>
      {files.map((file) => (
        <Text key={file.name}>{file.name}</Text>
      ))}
    </>
      </div>
      <button type="submit">Report Pet Lost</button>
    </form>
                </View>
                </div>
  );
};

export default HomePage;
