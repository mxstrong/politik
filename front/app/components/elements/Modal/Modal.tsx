import ReactModal, { Props as ReactModalProps } from 'react-modal';
import { BsX } from 'react-icons/bs';
import { ReactNode } from 'react';

import IconButton from '@element/IconButton';

interface IModal extends ReactModalProps {
  onClose: () => void;
  title?: ReactNode;
}

ReactModal.setAppElement('body');

const Modal: React.FC<IModal> = ({ onClose, title, children, ...props }) => {
  return (
    <ReactModal
      overlayClassName="fixed top-0 right-0 bottom-0 left-0 bg-black bg-opacity-30 flex items-center justify-center"
      className="absolute bg-white min-w-max min-h-max p-6 md:p-10 rounded max-w-screen-md w-11/12 md:w-full md:m-6 z-20"
      {...props}
    >
      <IconButton onClick={onClose} className="absolute right-4 top-4">
        <BsX className="w-6 h-6" />
      </IconButton>
      {title && (
        <div className="mb-10">
          <h1 className="font-semibold text-2xl md:text-3xl">{title}</h1>
        </div>
      )}
      {children}
    </ReactModal>
  );
};

export default Modal;
