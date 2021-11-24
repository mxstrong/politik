import Button from '@element/Button';

interface IAuthButtons {
  onLoginClick?: () => void;
  onRegisterClick?: () => void;
}

const AuthButtons: React.FC<IAuthButtons> = ({
  onLoginClick,
  onRegisterClick,
}) => {
  return (
    <div className="flex flex-col md:flex-row space-y-2 md:space-y-0 md:space-x-2 mt-8 md:mt-0">
      <Button
        variant="outlined"
        onClick={onLoginClick}
        className="w-min ml-2 md:ml-0"
      >
        Prisijungimas
      </Button>
      <Button onClick={onRegisterClick} className="w-min ml-2 md:ml-0">
        Registracija
      </Button>
    </div>
  );
};

export default AuthButtons;
