import cx from 'classnames';

interface ISpin extends React.HTMLAttributes<HTMLDivElement> {
  size?: 'small' | 'medium';
}

const Spin: React.FC<ISpin> = ({ size = 'medium', className }) => {
  return (
    <div className={cx('flex', 'justify-center', 'items-center', className)}>
      <div
        className={cx(
          'animate-spin',
          'rounded-full',
          'border-t-2',
          'border-b-2',
          'border-primary',
          {
            'h-32': size === 'medium',
            'w-32': size === 'medium',
            'h-4': size === 'small',
            'w-4': size === 'small',
          }
        )}
      ></div>
    </div>
  );
};

export default Spin;
